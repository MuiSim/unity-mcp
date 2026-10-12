---
title: execute_custom_tool
sidebar_label: execute_custom_tool
description: "Execute a project-scoped custom tool registered by Unity."
---

# `execute_custom_tool`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.execute_custom_tool`

## Description

Execute a project-scoped custom tool registered by Unity.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `tool_name` | `str` | yes |  |
| `parameters` | `dict[str, Any] \| None` | — |  |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Invoke a project-scoped custom tool registered by Unity.
**Prerequisite:** select an active Unity instance and read its custom-tools
resource to discover the exact tool name, parameter schema and side effects.
Pass this object to `execute_custom_tool`:

```json
{"tool_name":"<registered-custom-tool-name>","parameters":{}}
```

The name is a placeholder. `{}` is appropriate only for a discovered parameterless
tool; otherwise fill `parameters` from that tool's schema. This is not a mechanism
for invoking arbitrary built-in MCP tools. **Verify:** inspect the custom result
and its documented output/state. **Caution:** custom tools can mutate assets or
perform external operations; inspect their implementation/description first.
<!-- examples:end -->
