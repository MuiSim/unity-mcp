---
title: manage_tools
sidebar_label: manage_tools
description: "Manage which tool groups are visible in this session."
---

# `manage_tools`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_tools`

## Description

Manage which tool groups are visible in this session. Actions: list_groups (show all groups and their status), activate (enable a group), deactivate (disable a group), sync (refresh visibility from Unity Editor's toggle states), reset (restore defaults). Activating a group makes its tools appear; deactivating hides them. Use sync after toggling tools in the Unity Editor GUI.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `action` | `Literal['list_groups', 'activate', 'deactivate', 'sync', 'reset']` | yes | Action to perform. |
| `group` | `str \| None` | — | Group name (required for activate / deactivate). Valid groups: animation, asset_gen, core, docs, probuilder, profiling, scripting_ext, testing, ui, vfx |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Discover and enable optional tool groups; only `core` is enabled by default.
**Prerequisite:** a connected MCP client. Each block is a separate `manage_tools` call:

```json
{"action":"list_groups"}
```

Enable animation tools before using `manage_animation`:

```json
{"action":"activate","group":"animation"}
```

Disable them when finished:

```json
{"action":"deactivate","group":"animation"}
```

**Verify:** repeat `list_groups` and refresh a client that caches tool lists.
`reset` restores core-only visibility; it does not undo scene/asset changes.
<!-- examples:end -->
