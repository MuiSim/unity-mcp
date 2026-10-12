---
title: manage_texture
sidebar_label: manage_texture
description: "Procedural texture generation for Unity."
---

# `manage_texture`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `vfx` &nbsp;·&nbsp; **Module:** `services.tools.manage_texture`

## Description

Procedural texture generation for Unity. Creates textures with solid fills, patterns (checkerboard, stripes, dots, grid, brick), gradients, and noise. Actions: create, modify, delete, create_sprite, apply_pattern, apply_gradient, apply_noise, set_import_settings

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `action` | `Literal['create', 'modify', 'delete', 'create_sprite', 'apply_pattern', 'apply_gradient', 'apply_noise', 'set_import_settings']` | yes | Action to perform. |
| `path` | `str \| None` | — | Output texture path (e.g., 'Assets/Textures/MyTexture.png') |
| `width` | `int \| None` | — | Texture width in pixels (default: 64) |
| `height` | `int \| None` | — | Texture height in pixels (default: 64) |
| `fill_color` | `list[int \| float] \| dict[str, int \| float] \| str \| None` | — | Fill color as [r, g, b] or [r, g, b, a] array, {r, g, b, a} object, or hex string. Accepts both 0-255 range (e.g., [255, 0, 0]) or 0.0-1.0 normalized range (e.g., [1.0, 0, 0]) |
| `pattern` | `Literal['checkerboard', 'stripes', 'stripes_h', 'stripes_v', 'stripes_diag', 'dots', 'grid', 'brick'] \| None` | — | Pattern type for apply_pattern action |
| `palette` | `list[list[int \| float]] \| str \| None` | — | Color palette as [[r,g,b,a], ...]. Accepts both 0-255 range or 0.0-1.0 normalized range |
| `pattern_size` | `int \| None` | — | Pattern cell size in pixels (default: 8) |
| `pixels` | `list[list[int]] \| str \| None` | — | Pixel data as JSON array of [r,g,b,a] values or base64 string |
| `image_path` | `str \| None` | — | Source image file path for create/create_sprite (PNG/JPG). |
| `gradient_type` | `Literal['linear', 'radial'] \| None` | — | Gradient type (default: linear) |
| `gradient_angle` | `float \| None` | — | Gradient angle in degrees for linear gradient (default: 0) |
| `noise_scale` | `float \| None` | — | Noise scale/frequency (default: 0.1) |
| `octaves` | `int \| None` | — | Number of noise octaves for detail (default: 1) |
| `set_pixels` | `dict[Any] \| None` | — | Region to modify: {x, y, width, height, color or pixels} |
| `as_sprite` | `dict[Any] \| bool \| None` | — | Configure as sprite: {pivot: [x,y], pixels_per_unit: 100} or true for defaults |
| `import_settings` | `dict[Any] \| None` | — | TextureImporter settings dict. Keys: texture_type (default/normal_map/sprite/etc), texture_shape (2d/cube), srgb (bool), alpha_source (none/from_input/from_gray_scale), alpha_is_transparency (bool), readable (bool), generate_mipmaps (bool), wrap_mode/wrap_mode_u/wrap_mode_v (repeat/clamp/mirror/mirror_once), filter_mode (point/bilinear/trilinear), aniso_level (0-16), max_texture_size (32-16384), compression (none/low_quality/normal_quality/high_quality), compression_quality (0-100), sprite_mode (single/multiple/polygon), sprite_pixels_per_unit, sprite_pivot, sprite_mesh_type (full_rect/tight), sprite_extrude (0-32) |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Create or edit texture assets and import settings.
**Prerequisite:** activate `vfx` with `manage_tools`; there is no read action, so
inspect existing textures with `manage_asset` before mutations.
Call `manage_texture` to create a new example texture:

```json
{"action":"create","path":"Assets/Textures/ExampleWhite.png","width":32,"height":32,"fill_color":[255,255,255,255]}
```

Use a new destination path. This color uses 0–255 channel values.
After creation, set importer filtering:

```json
{"action":"set_import_settings","path":"Assets/Textures/ExampleWhite.png","import_settings":{"filterMode":"Point"}}
```

**Verify:** read asset info/preview with `manage_asset` and inspect dimensions and
import settings. **Destructive:** `delete` removes the asset; modifications and
procedural pattern/gradient/noise actions can replace pixel content. Back up first.
<!-- examples:end -->
