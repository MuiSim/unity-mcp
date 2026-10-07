<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../images/logo-header-dark.png">
    <img alt="MCP for Unity" src="../images/logo-header-light.png" width="400">
  </picture>
</p>

<div align="center">

[English](../../README.md) <img src="../images/connector.svg" alt="↔" height="14"> [简体中文](README-zh.md) &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; [Discord](https://discord.gg/y4p8KfzrN4) <img src="../images/connector.svg" alt="↔" height="14"> [Wiki](https://muisim.github.io/unity-mcp/)

#### 上游项目由 [Aura](https://www.tryaura.dev/) 荣誉赞助并维护 —— 面向 Unreal 与 Unity 的 AI 助手。
##### 别错过 [Godot AI](https://github.com/hi-godot/godot-ai) 🤖，MCP for Unity 团队推出的全新开源项目！

</div>

<p align="center">MCP for Unity 通过 <a href="https://modelcontextprotocol.io/introduction">Model Context Protocol</a> 把 Claude、Cursor、VS Code、本地大模型等 AI 助手接入 Unity 编辑器，让它们直接帮你管理资源、搭场景、写脚本、跑测试，把开发流程里的重复活儿都包了。</p>

**MuiSim 分支项目：** [代码仓库](https://github.com/MuiSim/unity-mcp.git)。
保留本地遥测事件创建，但禁用向远程服务器上报。

<p align="center">
  <img alt="MCP for Unity building a scene" src="../images/building_scene.gif">
</p>

---

<details>
<summary><strong>最近更新</strong></summary>

* **[v10.3.1](https://github.com/MuiSim/unity-mcp/tree/v10.3.1)**（2026-10-07）——固定 Unity 与 Python 服务器版本、禁用远程遥测上报，并使用本项目的更新与文档来源。

本项目的版本见 [Git 标签](https://github.com/MuiSim/unity-mcp/tags)。

</details>

---

## 它能做什么

用自然语言从任意 MCP 客户端操作 Unity 编辑器：搭场景、建 GameObject、写改 C# 脚本、调材质和着色器、跑测试、看性能、出包。50 个 MCP 工具入口，任意客户端可用，免费、MIT 开源。

**[查看完整工具目录 →](https://muisim.github.io/unity-mcp/reference/tools/)**

---

## 快速开始

**环境要求：** Unity **2021.3 LTS → 6.x** · Python **3.10+**（用 [`uv`](https://docs.astral.sh/uv/) 管理）。兼容**任意 MCP 客户端**——Claude Desktop 与 Claude Code、Cursor、VS Code、Windsurf、Cline、Gemini CLI 等等。

1. **安装** —— 在 Unity 里打开 Package Manager，从 git URL 添加：
   `https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#v10.3.1` &nbsp;_（使用 `#v10.3.1` 固定此分支项目的发布版本）_
2. **配置客户端** —— `Window → MCP for Unity → Configure All Detected Clients`，一键搞定所有检测到的客户端。
3. **发个提示试试** —— *"在原点放一个立方体，加个 Rigidbody。"* 立方体几秒就出现在场景里了。

版本标签会固定 Unity 包和默认 Python 服务器的版本。将 **Advanced Settings → Server Source Override**
留空即可自动使用对应标签；升级时请修改安装 URL 中的版本标签并重新生成客户端配置。
`#main` 和 `#beta` 跟随分支更新，不会固定版本。
默认服务器来源是
`https://github.com/MuiSim/unity-mcp/archive/v10.3.1.zip#subdirectory=Server`，
使用源码压缩包可避免 Python 服务器安装时的 Windows Git 长路径问题。

<details>
<summary><strong>手动配置</strong></summary>

如果自动配置不生效，把下面的内容加到你的 MCP 客户端配置文件里：

**HTTP（默认 —— 适用于 Claude Desktop、Cursor、Windsurf）：**
```json
{
  "mcpServers": {
    "unityMCP": {
      "url": "http://localhost:8080/mcp"
    }
  }
}
```

**VS Code：**
```json
{
  "servers": {
    "unityMCP": {
      "type": "http",
      "url": "http://localhost:8080/mcp"
    }
  }
}
```

<details>
<summary>Stdio 配置（uvx）</summary>

**macOS/Linux：**
```json
{
  "mcpServers": {
    "unityMCP": {
      "command": "uvx",
      "args": ["--from", "https://github.com/MuiSim/unity-mcp/archive/v10.3.1.zip#subdirectory=Server", "mcp-for-unity", "--transport", "stdio"]
    }
  }
}
```

**Windows：**
```json
{
  "mcpServers": {
    "unityMCP": {
      "command": "C:/Users/YOUR_USERNAME/AppData/Local/Microsoft/WinGet/Links/uvx.exe",
      "args": ["--from", "https://github.com/MuiSim/unity-mcp/archive/v10.3.1.zip#subdirectory=Server", "mcp-for-unity", "--transport", "stdio"]
    }
  }
}
```
</details>
</details>

---

<details>
<summary><strong>多个 Unity 实例</strong></summary>

MCP for Unity 支持同时开多个 Unity 编辑器实例。想把操作定向到某个实例：

1. 让大模型读一下 `unity_instances` 资源
2. 用 `set_active_instance` 传入 `Name@hash`（比如 `MyProject@abc123`）
3. 之后所有工具调用都会走这个实例
</details>

<details>
<summary><strong>Roslyn 脚本验证（进阶）</strong></summary>

想用能查出未定义命名空间、类型和方法的 **Strict** 验证：

1. 装 [NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity)
2. `Window > NuGet Package Manager` → 安装 `Microsoft.CodeAnalysis` v5.0
3. 再装 `SQLitePCLRaw.core` 和 `SQLitePCLRaw.bundle_e_sqlite3` v3.0.2
4. 在 `Player Settings > Scripting Define Symbols` 里加上 `USE_ROSLYN`
5. 重启 Unity

  <details>
  <summary>手动安装 DLL（NuGetForUnity 用不了时）</summary>

  1. 从 [NuGet](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp/) 下载 `Microsoft.CodeAnalysis.CSharp.dll` 及其依赖
  2. 把 DLL 放进 `Assets/Plugins/`
  3. 确认 .NET 兼容性设置正确
  4. 在 Scripting Define Symbols 里加上 `USE_ROSLYN`
  5. 重启 Unity
  </details>
</details>

<details>
<summary><strong>故障排除</strong></summary>

* **Unity Bridge 连不上：** 看一下 `Window > MCP for Unity` 的状态，重启 Unity
* **服务器起不来：** 确认 `uv --version` 能跑，并看看终端报错
* **客户端连不上：** 确认 HTTP 服务在运行，且 URL 和你的配置一致

**详细配置指南：**
* [故障排除](https://muisim.github.io/unity-mcp/guides/troubleshooting) —— uv/Python 安装、PATH 和常见问题
* [客户端配置指南](https://muisim.github.io/unity-mcp/guides/client-configurators) —— 各客户端配置说明

还是搞不定？[提个 Issue](https://github.com/MuiSim/unity-mcp/issues) 或者 [来上游 Discord 问](https://discord.gg/y4p8KfzrN4)
</details>

<details>
<summary><strong>参与贡献</strong></summary>

开发环境配置见 [README-DEV-zh.md](../development/README-DEV-zh.md)，自定义工具见 [CUSTOM_TOOLS.md](../reference/CUSTOM_TOOLS.md)。

1. Fork → 开 issue → 建分支（`feature/your-idea`）→ 改 → 提 PR
</details>

<details>
<summary><strong>遥测与隐私</strong></summary>

此分支项目允许创建本地遥测事件，但禁用所有远程遥测上报。`DISABLE_TELEMETRY=true`
仍可关闭本地事件收集。未来可以另行实现自有服务器或本地文件审查；当前均未启用。
详见 [遥测说明](https://muisim.github.io/unity-mcp/architecture/telemetry)。
</details>

---

**许可证：** MIT —— 见 [LICENSE](../../LICENSE) | **需要帮助？** [上游 Discord](https://discord.gg/y4p8KfzrN4) | [Issues](https://github.com/MuiSim/unity-mcp/issues)

---

## Star 历史

[![Star History Chart](https://star-history.dera.page/svg?repos=MuiSim/unity-mcp&type=Date)](https://star-history.dera.page/#MuiSim/unity-mcp&Date)

<details>
<summary><strong>论文引用</strong></summary>
如果 MCP for Unity 对你的研究有帮助，欢迎引用我们！

```bibtex
@inproceedings{10.1145/3757376.3771417,
author = {Wu, Shutong and Barnett, Justin P.},
title = {MCP-Unity: Protocol-Driven Framework for Interactive 3D Authoring},
year = {2025},
isbn = {9798400721366},
publisher = {Association for Computing Machinery},
address = {New York, NY, USA},
url = {https://doi.org/10.1145/3757376.3771417},
doi = {10.1145/3757376.3771417},
series = {SA Technical Communications '25}
}
```
</details>

## Aura 的 Unity AI 工具

Aura 出品两款 Unity AI 工具：
- **MCP for Unity** —— MIT 许可证，免费开源。
- **Aura for Unity** —— 面向游戏开发者的高级 Unity/Unreal AI 助手。

## 免责声明

本项目是一个免费开源的 Unity 编辑器工具，与 Unity Technologies 无关。
