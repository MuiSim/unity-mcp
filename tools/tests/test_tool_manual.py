"""Keep the stable per-tool manual aligned with registered MCP arguments."""

import inspect
import json
from pathlib import Path
import re
import sys
from typing import get_type_hints

import pytest
from pydantic import TypeAdapter

TOOLS_DIR = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(TOOLS_DIR))

import generate_docs_reference as docs


@pytest.fixture(scope="module")
def registered_tools():
    from services.registry import get_registered_tools

    # Other tests may already have imported modules that discovery will not reload.
    existing = {tool["name"]: tool for tool in get_registered_tools()}
    tools, _ = docs.load_registries()
    existing.update({tool["name"]: tool for tool in tools})
    return [
        tool for tool in existing.values()
        if tool["func"].__module__.startswith("services.tools.")
    ]


def test_manual_covers_exact_registered_tool_catalog(registered_tools):
    expected = {
        (tool.get("group") or "core", tool["name"])
        for tool in registered_tools
    }
    actual = {
        (path.parent.name, path.stem)
        for path in docs.TOOLS_OUT.glob("*/*.md")
        if path.stem != "index"
    }
    assert actual == expected


@pytest.mark.parametrize(
    "path",
    sorted(path for path in docs.TOOLS_OUT.glob("*/*.md") if path.stem != "index"),
    ids=lambda path: path.stem,
)
def test_every_tool_has_schema_valid_argument_examples(registered_tools, path):
    tool = next(
        entry for entry in registered_tools
        if entry["name"] == path.stem and (entry.get("group") or "core") == path.parent.name
    )
    examples = docs._read_existing_examples(path)
    assert "No examples yet" not in examples, tool["name"]
    blocks = re.findall(r"```json\s*\n(.*?)\n```", examples, re.DOTALL)
    assert blocks, f"{tool['name']}: missing JSON arguments"
    signature = inspect.signature(tool["func"])
    hints = get_type_hints(tool["func"], include_extras=True)
    parameters = {p.name for p in docs.introspect_params(tool["func"])}
    for block in blocks:
        arguments = json.loads(block)
        assert isinstance(arguments, dict), tool["name"]
        assert arguments.keys() <= parameters, (
            tool["name"], arguments.keys() - parameters
        )
        for name in parameters:
            parameter = signature.parameters[name]
            if parameter.default is inspect.Parameter.empty:
                assert name in arguments, (tool["name"], name)
            if name in arguments:
                TypeAdapter(hints[name]).validate_python(arguments[name])
        if tool["name"] == "batch_execute":
            names = {entry["name"] for entry in registered_tools}
            for command in arguments["commands"]:
                assert command["tool"] in names
                assert isinstance(command.get("params", {}), dict)


def test_usage_markers_do_not_corrupt_generated_banner(registered_tools):
    for tool in registered_tools:
        path = docs.TOOLS_OUT / (tool.get("group") or "core") / f"{tool['name']}.md"
        text = path.read_text(encoding="utf-8")
        assert docs.GENERATED_BANNER in text, tool["name"]
        for marker in (docs.EXAMPLES_OPEN, docs.EXAMPLES_CLOSE):
            assert len(re.findall(rf"^{re.escape(marker)}$", text, re.MULTILINE)) == 1
        assert text.index("\n" + docs.EXAMPLES_OPEN) > text.index("## Examples")


def test_regeneration_preserves_every_usage_block(registered_tools, tmp_path):
    for tool in registered_tools:
        path = docs.TOOLS_OUT / (tool.get("group") or "core") / f"{tool['name']}.md"
        examples = docs._read_existing_examples(path)
        regenerated = tmp_path / path.name
        regenerated.write_text(docs.render_tool_page(tool, examples), encoding="utf-8")
        assert docs._read_existing_examples(regenerated) == examples, tool["name"]


def test_agent_guide_is_linked_and_uses_registered_tools(registered_tools):
    guide_path = docs.WEBSITE_DOCS / "guides" / "agent-tool-manual.md"
    guide = guide_path.read_text(encoding="utf-8")
    for tool in registered_tools:
        group = tool.get("group") or "core"
        assert f"../reference/tools/{group}/{tool['name']}.md" in guide
    catalog = docs.render_catalog_index({}, {})
    assert "../../guides/agent-tool-manual.md" in catalog
    assert "coplaydev.github.io" not in guide
    assert "v10.3.0" in guide and "predates this fix" in guide


def test_agent_guide_uses_registered_resource_uris(registered_tools):
    from services.registry import get_registered_resources

    registered = {resource["uri"] for resource in get_registered_resources()}
    guide = (docs.WEBSITE_DOCS / "guides" / "agent-tool-manual.md").read_text(encoding="utf-8")
    for uri in re.findall(r"`(mcpforunity://[^`]+)`", guide):
        assert uri in registered, uri


def test_agent_guide_json_examples_match_tool_schemas(registered_tools):
    guide = (docs.WEBSITE_DOCS / "guides" / "agent-tool-manual.md").read_text(encoding="utf-8")
    blocks = re.findall(r"```json\s*\n(.*?)\n```", guide, re.DOTALL)
    names = ["find_gameobjects", "manage_gameobject", "batch_execute", "run_tests", "get_test_job"]
    assert len(blocks) == len(names)
    tools = {tool["name"]: tool for tool in registered_tools}
    for name, block in zip(names, blocks):
        arguments = json.loads(block)
        function = tools[name]["func"]
        hints = get_type_hints(function, include_extras=True)
        signature = inspect.signature(function)
        assert arguments.keys() <= signature.parameters.keys(), name
        for key, value in arguments.items():
            TypeAdapter(hints[key]).validate_python(value)
