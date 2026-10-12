---
title: manage_build
sidebar_label: manage_build
description: "Manage Unity player builds — trigger builds, switch platforms, configure settings, manage build scenes and profiles, run batch builds across platforms."
---

# `manage_build`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_build`

## Description

Manage Unity player builds — trigger builds, switch platforms, configure settings, manage build scenes and profiles, run batch builds across platforms. Actions: build, status, platform, settings, scenes, profiles, batch, cancel.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `action` | `str` | yes | Action: build, status, platform, settings, scenes, profiles, batch, cancel |
| `target` | `str \| None` | — | Build target: windows64, osx, linux64, android, ios, webgl, uwp, tvos, visionos |
| `output_path` | `str \| None` | — | Output path for the build |
| `scenes` | `str \| None` | — | JSON array of scene paths, or comma-separated paths |
| `development` | `str \| None` | — | Development build (true/false) |
| `options` | `str \| None` | — | JSON array of BuildOptions: clean_build, auto_run, deep_profiling, compress_lz4, strict_mode, detailed_report |
| `subtarget` | `str \| None` | — | Build subtarget: player or server |
| `scripting_backend` | `str \| None` | — | Scripting backend: mono or il2cpp (persistent change) |
| `profile` | `str \| None` | — | Build Profile asset path (Unity 6+ only) |
| `property` | `str \| None` | — | Settings property: product_name, company_name, version, bundle_id, scripting_backend, defines, architecture |
| `value` | `str \| None` | — | Value to set for the property (omit to read) |
| `activate` | `str \| None` | — | Activate a build profile (true/false) |
| `targets` | `str \| None` | — | JSON array of targets for batch build |
| `profiles` | `str \| None` | — | JSON array of profile paths for batch build (Unity 6+) |
| `output_dir` | `str \| None` | — | Base output directory for batch builds |
| `job_id` | `str \| None` | — | Job ID for status/cancel |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Inspect build configuration before a player build.
**Prerequisite:** a compiling project, saved scenes and installed platform modules.
Call `manage_build` with each object:

```json
{"action":"scenes"}
```

Read, without changing, the current product name:

```json
{"action":"settings","property":"product_name"}
```

Build an example saved scene to a new output location:

```json
{"action":"build","target":"windows64","output_path":"Builds/Example/Example.exe","scenes":"[\"Assets/Scenes/Example.unity\"]","development":"true"}
```

`scenes` and `development` are **strings** in this MCP schema. Replace example paths.
Poll the returned job ID (`<returned-job-id>` is a placeholder):

```json
{"action":"status","job_id":"<returned-job-id>"}
```

**Verify:** inspect the build report and output. **Caution:** builds write files;
platform/backend changes can persist and cause lengthy reimports.
<!-- examples:end -->
