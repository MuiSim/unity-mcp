---
title: create_script
sidebar_label: create_script
description: "Create a new C# script at the given project path."
---

# `create_script`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_script`

## Description

Create a new C# script at the given project path.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `path` | `str` | yes | Path under Assets/ to create the script at, e.g., 'Assets/Scripts/My.cs' |
| `contents` | `str` | yes | Contents of the script to create (plain text C# code). The server handles Base64 encoding. |
| `script_type` | `str \| None` | — | Script type (e.g., 'C#') |
| `namespace` | `str \| None` | — | Namespace for the script |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Create a complete C# source file. **Prerequisite:** a connected Editor and a new
destination under `Assets`; choose a class name matching the file name.
Pass this object to `create_script`:

```json
{"path":"Assets/Scripts/ExampleMover.cs","contents":"using UnityEngine;\npublic class ExampleMover : MonoBehaviour\n{\n    void Update()\n    {\n        transform.Translate(Vector3.forward * Time.deltaTime);\n    }\n}\n"}
```

`ExampleMover` and its path are examples; do not overwrite an existing script
without backing it up. **Verify:** use `manage_script` to read the new file,
request compilation with `refresh_unity`, wait for Editor readiness and inspect
console errors before adding the component to a GameObject.
<!-- examples:end -->
