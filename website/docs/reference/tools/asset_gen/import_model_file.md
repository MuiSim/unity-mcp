---
title: import_model_file
sidebar_label: import_model_file
description: "Import a local 3D model file that already exists on disk (e.g. an FBX/OBJ/glTF exported from Blender or another DCC tool) into the Unity project."
---

# `import_model_file`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `asset_gen` &nbsp;·&nbsp; **Module:** `services.tools.import_model_file`

## Description

Import a local 3D model file that already exists on disk (e.g. an FBX/OBJ/glTF exported from Blender or another DCC tool) into the Unity project. The file is copied under Assets/ and run through Unity's model-import pipeline (scale-normalize, material settings; glTF requires glTFast). Carries no API keys and no file bytes over the bridge.

Params: source_path (absolute or Assets-relative path to a .fbx/.obj/.glb/.gltf/.zip), name, output_folder (under Assets/), target_size, animation_type. Returns { asset_path, asset_guid }.

animation_type (FBX/OBJ only): pass 'generic' or 'humanoid' for a rigged/animated mesh so Unity surfaces its AnimationClips; omitted or 'none' imports no rig (this is the usual cause of a rigged FBX importing with zero clips); 'legacy' selects Unity's legacy Animation system (rarely needed). glTF/GLB ignore it — glTFast imports animation itself.

For multi-file exports (a text .gltf with an external .bin, or an .obj with a sibling .mtl/textures), zip them and pass the .zip — a bare .gltf/.obj is copied without its sidecars.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `source_path` | `str` | yes | Path to the model file on disk (.fbx/.obj/.glb/.gltf/.zip). |
| `name` | `str \| None` | — | Base name for the imported asset. |
| `output_folder` | `str \| None` | — | Destination folder under Assets/ for the import. |
| `target_size` | `float \| None` | — | Normalize the largest dimension to this size (meters). |
| `animation_type` | `Literal['none', 'generic', 'humanoid', 'legacy'] \| None` | — | FBX/OBJ only: rig/animation import mode. 'generic' or 'humanoid' surface the model's AnimationClips; 'legacy' selects Unity's legacy Animation system (rarely needed); omitted or 'none' imports no rig. Ignored for glTF/GLB. |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Import an existing local model without a generation API.
**Prerequisite:** activate `asset_gen`; the source must exist on the **Unity
Editor's machine**, not only on the MCP client's machine. Supported inputs are
`.fbx`, `.obj`, `.glb`, `.gltf` and `.zip`; glTF support requires a compatible importer.
Replace the example source path with your own and call `import_model_file`:

```json
{"source_path":"C:\\Models\\Example.fbx","name":"ExampleModel","output_folder":"Assets/Imported","target_size":1.0,"animation_type":"generic"}
```

`animation_type` is relevant to FBX rigs/clips. `target_size` normalizes the largest
dimension in meters. **Write effect:** copies/imports files under `Assets`; no
paid provider call or API key is required. **Verify:** inspect returned
`asset_path`/`asset_guid` and imported meshes/materials. The tool does **not** place
the model in the scene; instantiate the returned asset separately.
<!-- examples:end -->
