---
title: Agent tool manual
sidebar_label: Agent tool manual
description: Choose and call each stable MCP for Unity tool with the correct instance, schema, and verification.
---

# Agent tool manual

This manual covers the **MuiSim stable code line**, not the newer upstream beta
toolset. The [tool catalog](../reference/tools/index.md) is generated from this
checkout's Python registry: **49 tools in 10 groups**, with a separate usage
manual and JSON examples on every tool page. A tool missing from the connected
server's schema must not be assumed to exist simply because newer online docs
mention it. The current client schema is authoritative for installed versions.

Examples are **tool arguments**, not shell commands or complete JSON-RPC
messages. Call the named tool with the example object. Replace object names,
paths, IDs, job IDs, and provider settings with values discovered in your project.
Do not submit literal placeholders.

## Start with the correct Editor

1. Read `mcpforunity://instances` and choose the intended project. Call
   [`set_active_instance`](../reference/tools/core/set_active_instance.md) with
   its returned `Name@hash` or hash. Keep the same instance throughout a workflow.
   If a result looks wrong, use
   [`debug_request_context`](../reference/tools/core/debug_request_context.md)
   before changing the scene; instance selection belongs to this client/session.
2. Read `mcpforunity://editor/state` and wait while compiling or reloading.
   Check readiness/blocking advice rather than retrying mutations blindly.
3. Read `mcpforunity://project/info` and
   `mcpforunity://editor/prefab-stage` as needed; use `manage_scene`'s
   `get_hierarchy` action for a paged hierarchy. Determine rendering pipeline,
   installed packages, loaded scene context, and whether a prefab is being edited.
   See the [resource catalog](../reference/resources/index.md) for exact URIs.
4. Call [`manage_tools`](../reference/tools/core/manage_tools.md) with
   `{"action":"list_groups"}`. Enable a needed group with
   `{"action":"activate","group":"testing"}` (for example), then refresh the
   client's tool listing. HTTP starts with core enabled; Editor tool toggles
   and transport/session visibility can hide other groups. Activation does not
   install a missing Unity package, provider key, or custom tool.

Do not put `unity_instance` into arbitrary tool arguments. Use
`set_active_instance`; a batch cannot route different commands to different
instances.

## Find, inspect, change, verify

Call **`find_gameobjects`**:

```json
{"search_term":"Player","search_method":"by_name","include_inactive":true,"page_size":50}
```

It returns `data.instanceIDs`, not full objects. Read
`mcpforunity://scene/gameobject/{instance_id}` and, when needed,
`mcpforunity://scene/gameobject/{instance_id}/components`. Follow `nextCursor`
using the tool's `cursor` argument until there are no more pages. Names match
exactly, not as substrings; duplicate names can identify more than one object.

After inspecting the target, call **`manage_gameobject`** with the discovered ID
(replace `12345`):

```json
{"action":"modify","target":12345,"search_method":"by_id","position":[0,2,0]}
```

Read the resource again to verify the transform. For component changes, use
[`manage_components`](../reference/tools/core/manage_components.md), not
GameObject search or an invented `get_components` action. Inspect property names
and types before setting them.

**Lookup fix availability:** the stable branch now edits inactive objects
without activating them, infers numeric-string IDs, and resolves IDs across
loaded scenes. The immutable `v10.3.0` tag predates this fix. Install
`https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#muisim-stable-10-3-0-release`
to obtain it; leave Server Source Override empty, regenerate client configs,
and restart the server. Older servers may require ID strings rather than JSON
integers. Names/paths remain scoped to the active scene or prefab stage; use IDs
for other loaded scenes. IDs are session-local: rediscover after scene reloads,
object destruction, or Editor restarts. For all-digit names, request `by_name`
explicitly. A scene object is not the same as a prefab asset; use
[`manage_prefabs`](../reference/tools/core/manage_prefabs.md) to edit the asset.

## Select the smallest matching tool

| Work | Tools and their usage pages | Verify with |
|------|-----------------------------|-------------|
| Scene lifecycle and hierarchy | [manage_scene](../reference/tools/core/manage_scene.md) | Scene resources; inspect save/load paths before overwriting |
| Find and edit scene objects | [find_gameobjects](../reference/tools/core/find_gameobjects.md), [manage_gameobject](../reference/tools/core/manage_gameobject.md), [manage_components](../reference/tools/core/manage_components.md) | GameObject/component resources |
| Assets, prefabs, materials | [manage_asset](../reference/tools/core/manage_asset.md), [manage_prefabs](../reference/tools/core/manage_prefabs.md), [manage_material](../reference/tools/core/manage_material.md) | Asset results, object components, console |
| C# creation and edits | [create_script](../reference/tools/core/create_script.md), [get_sha](../reference/tools/core/get_sha.md), [find_in_file](../reference/tools/core/find_in_file.md), [script_apply_edits](../reference/tools/core/script_apply_edits.md), [apply_text_edits](../reference/tools/core/apply_text_edits.md), [validate_script](../reference/tools/core/validate_script.md), [delete_script](../reference/tools/core/delete_script.md) | Compilation state, diagnostics, console |
| Legacy script routing | [manage_script](../reference/tools/core/manage_script.md), [manage_script_capabilities](../reference/tools/core/manage_script_capabilities.md) | Capabilities first; prefer dedicated script tools |
| Editor control and refresh | [manage_editor](../reference/tools/core/manage_editor.md), [refresh_unity](../reference/tools/core/refresh_unity.md), [execute_menu_item](../reference/tools/core/execute_menu_item.md), [read_console](../reference/tools/core/read_console.md) | Editor state and errors; menu item paths must exist |
| Cameras, rendering, physics | [manage_camera](../reference/tools/core/manage_camera.md), [manage_graphics](../reference/tools/core/manage_graphics.md), [manage_physics](../reference/tools/core/manage_physics.md) | Ping/status, scene resources, screenshots |
| Builds and packages | [manage_build](../reference/tools/core/manage_build.md), [manage_packages](../reference/tools/core/manage_packages.md) | Poll returned jobs/operations; platform modules and package availability |
| Animation and UI | [manage_animation](../reference/tools/animation/manage_animation.md), [manage_ui](../reference/tools/ui/manage_ui.md) | Animator state; UXML/USS and UIDocument results |
| VFX and procedural assets | [manage_vfx](../reference/tools/vfx/manage_vfx.md), [manage_shader](../reference/tools/vfx/manage_shader.md), [manage_texture](../reference/tools/vfx/manage_texture.md) | Ping/package requirements, imports and console |
| 3D mesh modeling | [manage_probuilder](../reference/tools/probuilder/manage_probuilder.md) | Requires `com.unity.probuilder`; inspect returned meshes |
| Live API and documentation | [unity_reflect](../reference/tools/docs/unity_reflect.md), [unity_docs](../reference/tools/docs/unity_docs.md) | Reflection in the connected Editor beats assumptions from another version |
| Testing and profiling | [run_tests](../reference/tools/testing/run_tests.md), [get_test_job](../reference/tools/testing/get_test_job.md), [manage_profiler](../reference/tools/profiling/manage_profiler.md) | Final job result and failure details, not just job acceptance |
| Extended scripting | [manage_scriptable_object](../reference/tools/scripting_ext/manage_scriptable_object.md), [execute_code](../reference/tools/scripting_ext/execute_code.md) | Serialized property paths, console; arbitrary code runs with Editor privileges |
| Project custom tools | [execute_custom_tool](../reference/tools/core/execute_custom_tool.md) | Discover `mcpforunity://custom-tools`; names/parameters are project-specific |
| Generated/imported media | [generate_model](../reference/tools/asset_gen/generate_model.md), [generate_image](../reference/tools/asset_gen/generate_image.md), [generate_audio](../reference/tools/asset_gen/generate_audio.md), [import_model](../reference/tools/asset_gen/import_model.md), [import_model_file](../reference/tools/asset_gen/import_model_file.md), [blender_bridge](../reference/tools/asset_gen/blender_bridge.md) | Provider/Blender readiness, poll completion, imported asset paths and licenses |
| Session visibility/routing and batching | [manage_tools](../reference/tools/core/manage_tools.md), [set_active_instance](../reference/tools/core/set_active_instance.md), [debug_request_context](../reference/tools/core/debug_request_context.md), [batch_execute](../reference/tools/core/batch_execute.md) | Inspect session state and each individual batch result |

## Argument conventions that prevent failed calls

- Use the **Python MCP parameter names** in each tool's parameter table, typically
  `search_method`, `include_inactive`, `component_type`, and `page_size`. Actions
  are tool-specific: do not invent universal `get`, `list`, or `modify` actions.
- Use native JSON booleans/numbers/arrays/objects when the schema permits them,
  not double-encoded JSON. Vectors are usually `[x,y,z]`, rotations Euler degrees,
  and colors `[r,g,b,a]`; verify the specific tool's schema and coordinate space.
- Unity asset paths usually start with `Assets/` and use `/`. A local imported
  model file instead needs an absolute path visible on the **Unity Editor host**,
  not just the agent/client machine. Script tools accepting URIs document their
  supported URI/path formats individually.
- `batch_execute` forwards inner `params` to Unity handlers, not through each
  Python tool normalizer. Use the handler's Unity parameter names in its inner
  payloads (for example `searchMethod`, `searchTerm`, `includeInactive`).
  Prefer its page's verified examples over converting arbitrary MCP calls
  mechanically. Server-only tools such as `manage_tools` and `set_active_instance`
  must be called separately.
- A response may contain `success:false`, `error`, or nested failed command
  results despite successful MCP transport. Read it before proceeding.
  Paginated results and async job acceptance are not final success.

## Batch only independent operations

For independent object creation, call **`batch_execute`**:

```json
{
  "commands":[
    {"tool":"manage_gameobject","params":{"action":"create","name":"ManualCubeA","primitiveType":"Cube"}},
    {"tool":"manage_gameobject","params":{"action":"create","name":"ManualCubeB","primitiveType":"Cube"}}
  ],
  "fail_fast":true
}
```

Default batch limit is 25, configurable up to 100. `parallel` is a scheduling
hint, not a guarantee. A batch is **not transactional**: earlier changes remain
if a later command fails. Do not batch an ID-dependent modification before you
have read the create/search response; split that into sequential calls.

## Scripts and tests: wait for the actual outcome

Before editing an existing script, inspect it with the client's resource/file
reading facility and [`find_in_file`](../reference/tools/core/find_in_file.md);
use [`get_sha`](../reference/tools/core/get_sha.md) to obtain the edit guard.
Prefer structured [`script_apply_edits`](../reference/tools/core/script_apply_edits.md)
over raw offsets. Script writes can trigger compilation: wait for editor
readiness and call [`read_console`](../reference/tools/core/read_console.md)
for compiler errors. `validate_script` diagnostics alone do not prove the whole
project compiles. Do not attach a new component until compilation succeeds.

Enable the testing group, then call **`run_tests`**:

```json
{"mode":"EditMode"}
```

Use the returned `job_id` with **`get_test_job`** (replace `returned-job-id`):

```json
{"job_id":"returned-job-id","wait_timeout":60,"include_failed_tests":true}
```

Continue until terminal status, then report pass/failure counts and failure
details. Keep the same Editor instance. For builds and generated media, use
their own poll/status actions instead of `get_test_job`.

## Dependencies, costs, and safe stopping

ProBuilder and Cinemachine actions need their packages; VFX Graph and volume
actions need compatible installed packages/render pipelines. UI Toolkit UXML/USS
is not the same API as uGUI Canvas components. AI generation needs explicitly
configured provider credentials and can incur charges/send prompts remotely;
the fork's disabled telemetry does **not** disable intentional provider requests.
Blender bridge needs a running BlenderMCP addon. Imports still require appropriate
asset licensing.

Never solve an unknown target by deleting/recreating the user's scene or changing
instance selection at random. Check inactive parents, exact names, selected
instance, prefab stage, loaded scene, stale IDs, and compilation state first.
On a schema error, consult that tool's page and current tool listing. On a
missing dependency, report the required package/configuration rather than
inventing a successful fallback.
