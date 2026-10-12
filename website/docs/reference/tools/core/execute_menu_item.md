---
title: execute_menu_item
sidebar_label: execute_menu_item
description: "Execute a Unity menu item by path."
---

# `execute_menu_item`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.execute_menu_item`

## Description

Execute a Unity menu item by path.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `menu_path` | `str \| None` | — | Menu path for 'execute' or 'exists' (e.g., 'File/Save Project') |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Invoke an existing Unity Editor menu command.
**Prerequisite:** a connected Editor and the exact menu path from its menu-items
resource. Pass this object to `execute_menu_item` to open the Console:

```json
{"menu_path":"Window/General/Console"}
```

**Verify:** inspect the response and confirm the Console window opens.
Menu paths vary by Editor version/packages and some commands require selection.
**Caution:** other menu commands can delete assets, build files or open blocking
dialogs; inspect the command before invoking it. This tool takes `menu_path`,
not an invented generic `action`.
<!-- examples:end -->
