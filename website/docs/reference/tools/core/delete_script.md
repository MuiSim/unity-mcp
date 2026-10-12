---
title: delete_script
sidebar_label: delete_script
description: "Delete a C# script by URI or Assets-relative path."
---

# `delete_script`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_script`

## Description

Delete a C# script by URI or Assets-relative path.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `uri` | `str` | yes | URI of the script to delete under Assets/ directory, mcpforunity://path/Assets/... or file://... or Assets/... |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Remove a C# script asset. **Prerequisite:** read the target and find references
before deleting; back up or commit it first. The path below is an example to replace.
Pass this object to `delete_script` only after confirming the target:

```json
{"uri":"Assets/Scripts/UnusedExample.cs"}
```

**Destructive:** deletion removes source and can leave missing components or break
compilation. Do not assume it can be undone through Editor Undo.
**Verify:** confirm the asset no longer exists, request compilation with
`refresh_unity`, and inspect console errors/missing script references.
<!-- examples:end -->
