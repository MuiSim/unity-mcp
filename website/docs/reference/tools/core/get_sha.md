---
title: get_sha
sidebar_label: get_sha
description: "Get SHA256 and basic metadata for a Unity C# script without returning file contents."
---

# `get_sha`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_script`

## Description

Get SHA256 and basic metadata for a Unity C# script without returning file contents. Requires uri (script path under Assets/ or mcpforunity://path/Assets/... or file://...).

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `uri` | `str` | yes | URI of the script to edit under Assets/ directory, mcpforunity://path/Assets/... or file://... or Assets/... |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Read a script's current SHA-256 without transferring its full contents.
**Prerequisite:** an existing script in the connected project.
Pass this object to `get_sha`, replacing the example path:

```json
{"uri":"Assets/Scripts/ExampleMover.cs"}
```

**Verify:** copy the returned hash into `apply_text_edits.precondition_sha256`.
Read the script separately to establish exact edit coordinates. A hash detects
drift; it does not validate C# semantics. Recompute after any concurrent edit.
<!-- examples:end -->
