---
title: manage_vfx
sidebar_label: manage_vfx
description: "Manage Unity VFX components (ParticleSystem, VisualEffect, LineRenderer, TrailRenderer)."
---

# `manage_vfx`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `vfx` &nbsp;·&nbsp; **Module:** `services.tools.manage_vfx`

## Description

Manage Unity VFX components (ParticleSystem, VisualEffect, LineRenderer, TrailRenderer). Action prefixes: particle_*, vfx_*, line_*, trail_*. Action-specific parameters go in `properties` (keys match ManageVFX.cs).

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `action` | `str` | yes | Action to perform (prefix: particle_, vfx_, line_, trail_). |
| `target` | `str \| None` | — | Target GameObject (name/path/id). |
| `search_method` | `Literal['by_id', 'by_name', 'by_path', 'by_tag', 'by_layer'] \| None` | — | How to find the target GameObject. |
| `properties` | `dict[str, Any] \| str \| None` | — | Action-specific parameters (dict or JSON string). |
| `component_index` | `int \| None` | — | Zero-based index to select which component when multiple of the same type exist (e.g., multiple ParticleSystems). If omitted, targets the first instance. |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Inspect/control particle systems, VFX Graphs, lines and trails.
**Prerequisite:** activate `vfx` with `manage_tools`; VFX Graph actions additionally
require its package and a compatible pipeline. Call `manage_vfx`:

```json
{"action":"ping"}
```

Inspect an existing example ParticleSystem target before changing modules:

```json
{"action":"particle_get_info","target":"ExampleParticles","search_method":"by_name"}
```

Inspect available VFX Graph templates:

```json
{"action":"vfx_list_templates"}
```

**Verify:** inspect returned modules/templates and observe playback in the intended
scene. Replace `ExampleParticles` with a verified object; use `component_index`
for repeated particle components. **Caution:** clear actions discard live
particles/line/trail data, and asset/module edits can change shared visual effects.
<!-- examples:end -->
