---
title: set_active_instance
sidebar_label: set_active_instance
description: "Set the active Unity instance for this client/session."
---

# `set_active_instance`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.set_active_instance`

## Description

Set the active Unity instance for this client/session. Accepts Name@hash, hash prefix, or port number (stdio only).

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `instance` | `str` | yes | Target instance (Name@hash, hash prefix, or port number in stdio mode) |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Route subsequent commands to the intended connected Editor.
**Prerequisite:** read the Unity-instances resource and copy a real identifier.
Call `set_active_instance`; `<name@hash>` is a placeholder:

```json
{"instance":"<name@hash>"}
```

**Verify:** query `manage_scene` with `action:"get_active"` and confirm the project
and scene before mutations. Prefer the full identifier over an ambiguous name.
<!-- examples:end -->
