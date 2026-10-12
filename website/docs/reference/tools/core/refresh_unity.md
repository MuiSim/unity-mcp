---
title: refresh_unity
sidebar_label: refresh_unity
description: "Request a Unity asset database refresh and optionally a script compilation."
---

# `refresh_unity`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.refresh_unity`

## Description

Request a Unity asset database refresh and optionally a script compilation. Can optionally wait for readiness.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `mode` | `Literal['if_dirty', 'force']` | — | Refresh mode |
| `scope` | `Literal['assets', 'scripts', 'all']` | — | Refresh scope |
| `compile` | `Literal['none', 'request']` | — | Whether to request compilation |
| `wait_for_ready` | `bool` | — | If true, wait until mcpforunity://editor/state reports data.advice.ready_for_tools true |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Refresh imports and request compilation after external edits.
**Prerequisite:** save files in the connected project. Call `refresh_unity`:

```json
{"mode":"if_dirty","scope":"all","compile":"request","wait_for_ready":true}
```

Asset-only refresh without requesting compilation:

```json
{"mode":"if_dirty","scope":"assets","compile":"none","wait_for_ready":true}
```

**Verify:** inspect readiness and read console errors. Refresh completion does
not prove compilation succeeded. Wait through domain reload before dependent calls.
<!-- examples:end -->
