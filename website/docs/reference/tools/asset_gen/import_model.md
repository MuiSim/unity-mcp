---
title: import_model
sidebar_label: import_model
description: "Import 3D models from the Sketchfab marketplace into the Unity project."
---

# `import_model`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `asset_gen` &nbsp;·&nbsp; **Module:** `services.tools.import_model`

## Description

Import 3D models from the Sketchfab marketplace into the Unity project. Bring-your-own-key: the Sketchfab token lives in the editor's secure store and never crosses the bridge.

ACTIONS:
- search: Search Sketchfab. Params: query, categories, downloadable, count, cursor -> results with model uids.
- preview: Fetch model metadata (name, thumbnail URLs, license, vertex/face counts) for a uid before import.
- import: Download + import a model by uid. Returns { job_id } immediately; poll with the status action. Params: uid, target_size, name, output_folder.
- status: Poll an async import job by job_id -> { state, progress, assetPath?, error? }.
- cancel: Cancel an in-flight import by job_id.
- list_providers: List configured marketplace providers (no key values).

## Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `action` | `Literal['search', 'preview', 'import', 'status', 'cancel', 'list_providers']` | yes | Action to perform. |
| `query` | `str \| None` | — | Search query for the search action. |
| `categories` | `str \| None` | — | Filter search by category. |
| `downloadable` | `bool \| None` | — | Restrict search to downloadable models. |
| `count` | `int \| None` | — | Maximum number of search results. |
| `cursor` | `str \| None` | — | Pagination cursor for search. |
| `uid` | `str \| None` | — | Sketchfab model uid for preview/import. |
| `target_size` | `float \| None` | — | Normalize the largest dimension to this size (meters). |
| `name` | `str \| None` | — | Base name for the imported asset. |
| `output_folder` | `str \| None` | — | Destination folder under Assets/ for the import. |
| `job_id` | `str \| None` | — | Job id for status/cancel. |

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Search Sketchfab, inspect a model's license and import it.
**Prerequisite:** activate `asset_gen`, configure the Sketchfab token in the
Editor's secure store, and ensure network access. No token belongs in arguments.
Each object is a separate `import_model` call:

```json
{"action":"list_providers"}
```

Search downloadable models:

```json
{"action":"search","query":"wooden crate","downloadable":true,"count":5}
```

Preview a UID copied from search (`<model-uid>` is a placeholder):

```json
{"action":"preview","uid":"<model-uid>"}
```

Only after checking the license and attribution/download rights:

```json
{"action":"import","uid":"<model-uid>","target_size":1.0,"name":"ImportedCrate","output_folder":"Assets/Imported"}
```

**External/write effect:** search/preview/download contact Sketchfab; importing
writes assets. This is not a paid AI-generation call, but access and license terms
still apply. **Verify:** poll `status` using the returned `job_id`, inspect
`assetPath`/`error` and mesh/materials. Keep required attribution with your project.
<!-- examples:end -->
