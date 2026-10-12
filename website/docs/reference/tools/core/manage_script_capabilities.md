---
title: manage_script_capabilities
sidebar_label: manage_script_capabilities
description: "Get manage_script capabilities (supported ops, limits, and guards)."
---

# `manage_script_capabilities`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.manage_script`

## Description

Get manage_script capabilities (supported ops, limits, and guards).
    Returns:
        - ops: list of supported structured ops
        - text_ops: list of supported text ops
        - max_edit_payload_bytes: server edit payload cap
        - guards: header/using guard enabled flag

## Parameters

_No parameters._

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Discover supported script-edit operations and validation capabilities.
**Prerequisite:** a connected MCP client/Editor. This tool has no arguments;
call `manage_script_capabilities` with:

```json
{}
```

**Verify:** inspect the returned operations and options before constructing
`script_apply_edits` or `apply_text_edits` requests. Capabilities are version-specific;
do not assume an operation from another release is available.
<!-- examples:end -->
