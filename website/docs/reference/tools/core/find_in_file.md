---
title: find_in_file
sidebar_label: find_in_file
description: "Searches a file with a regex pattern and returns line numbers and excerpts."
---

# `find_in_file`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.find_in_file`

## Description

Searches a file with a regex pattern and returns line numbers and excerpts.

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `uri` | `str` | yes | The resource URI to search under Assets/ or file path form supported by read_resource |
| `pattern` | `str` | yes | The regex pattern to search for |
| `project_root` | `str \| None` | — | Optional project root path |
| `max_results` | `int` | — | Cap results to avoid huge payloads |
| `ignore_case` | `bool \| str \| None` | — | Case insensitive search |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Find regex matches in a script without editing it.
**Prerequisite:** an existing project file; replace the example URI.
Pass this object to `find_in_file`:

```json
{"uri":"Assets/Scripts/ExampleMover.cs","pattern":"\\bUpdate\\s*\\(","max_results":20,"ignore_case":false}
```

**Verify:** inspect returned match positions/context before constructing edits.
JSON requires doubled backslashes in regex strings. A capped result set is not
proof there are no further matches; narrow the pattern if needed.
<!-- examples:end -->
