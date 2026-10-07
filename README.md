<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="docs/images/logo-header-dark.png">
    <img alt="MCP for Unity" src="docs/images/logo-header-light.png" width="400">
  </picture>
</p>

<div align="center">

[English](README.md) <img src="docs/images/connector.svg" alt="↔" height="14"> [简体中文](docs/i18n/README-zh.md) &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; [Discord](https://discord.gg/y4p8KfzrN4) <img src="docs/images/connector.svg" alt="↔" height="14"> [Wiki](https://muisim.github.io/unity-mcp/)

#### Upstream sponsored and maintained by [Aura](https://www.tryaura.dev/) — the AI assistant for Unreal & Unity.
##### And don't miss [Godot AI](https://github.com/hi-godot/godot-ai), the new open source project from the makers of MCP for Unity.

</div>

<p align="center"><b>Create your Unity apps with LLMs.</b> MCP for Unity bridges AI assistants — Claude, Codex, VS Code, local LLMs, and more — with your Unity Editor via <a href="https://modelcontextprotocol.io/introduction">Model Context Protocol</a>. Give your LLM the tools to manage assets, control scenes, edit scripts, run tests, and automate your game dev workflows.</p>

**MuiSim fork:** [Repository](https://github.com/MuiSim/unity-mcp.git) ·
[Version tags](https://github.com/MuiSim/unity-mcp/tags). This fork preserves local
telemetry creation while disabling remote reporting.

<p align="center">
  <img alt="MCP for Unity building a scene" src="docs/images/building_scene.gif">
</p>

---

<!-- recent-updates:start -->
<details>
<summary><strong>Recent Updates</strong></summary>

* **[v10.3.1](https://github.com/MuiSim/unity-mcp/tree/v10.3.1)** (2026-10-07) — version-pinned Unity and Python server installs, remote telemetry reporting disabled, and fork update/documentation sources.

Fork versions: [Git tags](https://github.com/MuiSim/unity-mcp/tags).

</details>
<!-- recent-updates:end -->

---

## What it does

Control the Unity Editor in natural language from any MCP client — create scenes & GameObjects, edit C# scripts, manage assets, run tests, profile, and build. 50 focused MCP tool entrypoints, any client, free & MIT.

**[Browse the full tool catalog →](https://muisim.github.io/unity-mcp/reference/tools/)**

---

## Quickstart

**Requirements:** Unity **2021.3 LTS → 6.x** · Python **3.10+** (via [`uv`](https://docs.astral.sh/uv/)). Works with **any MCP client** — Claude Desktop & Code, Cursor, VS Code, Windsurf, Cline, Gemini CLI, and more.

1. **Install** — Unity → Package Manager → Add from git URL:
   `https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#v10.3.1` &nbsp;_(pin `#v10.3.1` for this fork release)_
2. **Configure** — `Window → MCP for Unity → Configure All Detected Clients`.
3. **Prompt** — *"Create a cube at the origin and add a Rigidbody."* The cube appears in seconds.

### Pinning the install version

`#v10.3.1` selects a fixed release instead of a moving branch. Unity keeps that
version until you change the Git URL to another release tag. The default Python
server source uses the same tag automatically:
`https://github.com/MuiSim/unity-mcp/archive/v10.3.1.zip#subdirectory=Server`.
Leave **Advanced Settings → Server Source Override** empty to use this matching
version. The source archive avoids Windows Git checkout long-path failures when
installing the Python server.

You can also track `#main` or `#beta`; those are moving branches, not version pins.
The matching Python server follows the selected branch. These fork changes are
available from `#v10.3.1` independently of when they are merged into those branches.

### After installing

This fork preserves telemetry event creation and local collection, but disables
remote reporting in the Python server and Unity bridge. Its documentation site
also disables the analytics beacon. No owner-controlled server or local event-file
exporter is enabled yet.

Package update checks use `MuiSim/unity-mcp` on GitHub, checking `main` or `beta`
as appropriate. Update notifications do not change a pinned installation.

If upgrading an existing installation, replace any upstream server-source
override (clear it to use the matching version), regenerate MCP client
configurations, and restart the MCP server/client.
Existing configurations may still launch the upstream PyPI server, which does not
inherit this fork's reporting policy. Changes installed from this fork apply to
that build, not to separately installed upstream packages.

---

## Community

- [Discord](https://discord.gg/y4p8KfzrN4) — upstream community
- [Issues](https://github.com/MuiSim/unity-mcp/issues) — fork bugs and feature requests
- [Repository](https://github.com/MuiSim/unity-mcp.git) — source code and contributions
- Security: see [SECURITY.md](SECURITY.md) for private reporting

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Branch off `beta`, not `main`. The full dev setup, testing, and release process live in the [Contributing](https://muisim.github.io/unity-mcp/contributing/dev-setup) docs.

## Advanced

- **Multiple Unity instances** — [Multi-Instance Routing](https://muisim.github.io/unity-mcp/guides/multi-instance)
- **Tool groups (vfx / animation / ui / testing / etc.)** — [Tool Groups](https://muisim.github.io/unity-mcp/guides/tool-groups)
- **v10 asset generation and upgrade notes** — [v10 Migration](https://muisim.github.io/unity-mcp/migrations/v10)
- **Roslyn script validation** — [Roslyn Validation](https://muisim.github.io/unity-mcp/guides/roslyn)
- **Remote-hosted server with auth** — [Remote Server Auth](https://muisim.github.io/unity-mcp/guides/remote-server-auth)

## Star History

[![Star History Chart](https://star-history.dera.page/svg?repos=MuiSim/unity-mcp&type=Date)](https://star-history.dera.page/#MuiSim/unity-mcp&Date)

## Citation

If MCP for Unity helped your research, please cite it.

```bibtex
@inproceedings{wu2025mcpunity,
  author    = {Wu, Shutong and Barnett, Justin P.},
  title     = {{MCP-Unity}: {Protocol-Driven} Framework for Interactive {3D} Authoring},
  year      = {2025},
  isbn      = {9798400721366},
  publisher = {Association for Computing Machinery},
  address   = {New York, NY, USA},
  url       = {https://doi.org/10.1145/3757376.3771417},
  doi       = {10.1145/3757376.3771417},
  series    = {SA Technical Communications '25}
}
```

## Unity AI Tools by Aura

Aura offers 2 AI tools for Unity:
- **MCP for Unity** is available freely under the MIT license.
- **Aura for Unity** is a premium Unity/Unreal AI assistant built for game devs.

## Disclaimer

This project is a free and open-source tool for the Unity Editor, and is not affiliated with Unity Technologies.

---

**License:** MIT — see [LICENSE](LICENSE).
