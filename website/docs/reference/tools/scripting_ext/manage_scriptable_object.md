---
title: manage_scriptable_object
sidebar_label: manage_scriptable_object
description: "Creates and modifies ScriptableObject assets using Unity SerializedObject property paths."
---

# `manage_scriptable_object`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `scripting_ext` &nbsp;·&nbsp; **Module:** `services.tools.manage_scriptable_object`

## Description

Creates and modifies ScriptableObject assets using Unity SerializedObject property paths.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `action` | `Literal['create', 'modify']` | yes | Action to perform: create or modify. |
| `type_name` | `str \| None` | — | Namespace-qualified ScriptableObject type name (for create). |
| `folder_path` | `str \| None` | — | Target folder under Assets/... (for create). |
| `asset_name` | `str \| None` | — | Asset file name without extension (for create). |
| `overwrite` | `bool \| str \| None` | — | If true, overwrite existing asset at same path (for create). |
| `target` | `dict[str, Any] \| str \| None` | — | Target asset reference {guid\|path} (for modify). |
| `patches` | `list[dict[str, Any]] \| str \| None` | — | Patch list (or JSON string) to apply. For object references: use {"ref": {"guid": "..."}} or {"value": {"guid": "..."}}. For Sprite sub-assets: include "spriteName" in the ref/value object. Single-sprite textures auto-resolve from guid/path alone. |
| `dry_run` | `bool \| str \| None` | — | If true, validate patches without applying (modify only). |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Create ScriptableObject assets or patch their serialized properties.
**Prerequisite:** activate `scripting_ext` with `manage_tools`; compile the custom
ScriptableObject type and inspect the existing asset/property paths first.
Call `manage_scriptable_object` with a dry run before applying changes:

```json
{"action":"modify","target":{"path":"Assets/Data/ExampleSettings.asset"},"patches":[{"propertyPath":"m_Name","op":"set","value":"ExampleSettings"}],"dry_run":true}
```

The path is an example to replace. `m_Name` is the Unity serialized object name;
use your actual serialized fields for application data. After validation succeeds:

```json
{"action":"modify","target":{"path":"Assets/Data/ExampleSettings.asset"},"patches":[{"propertyPath":"m_Name","op":"set","value":"ExampleSettings"}],"dry_run":false}
```

**Verify:** inspect per-patch results and reread the asset. **Caution:** writes
change the shared asset; `overwrite:true` on creation can discard existing data.
`dry_run` validates modifications only; it does not preview creation.
<!-- examples:end -->
