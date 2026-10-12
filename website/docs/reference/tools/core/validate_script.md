---
title: validate_script
sidebar_label: validate_script
description: "Validate a C# script and return diagnostics."
---

# `validate_script`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_script`

## Description

Validate a C# script and return diagnostics.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `uri` | `str` | yes | URI of the script to validate under Assets/ directory, mcpforunity://path/Assets/... or file://... or Assets/... |
| `level` | `Literal['basic', 'standard']` | — | Validation level |
| `include_diagnostics` | `bool` | — | Include full diagnostics and summary |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Validate a C# script and request diagnostics without rewriting it.
**Prerequisite:** an existing script in the connected project.
Call `validate_script`, replacing the example URI:

```json
{"uri":"Assets/Scripts/ExampleMover.cs","level":"standard","include_diagnostics":true}
```

Use `level:"basic"` for a cheaper check. **Verify:** inspect success and individual
diagnostics, then request compilation/read console errors to catch project-wide
issues. Validation is not a substitute for compiling against actual project
dependencies and Unity-version APIs.
<!-- examples:end -->
