---
title: debug_request_context
sidebar_label: debug_request_context
description: "Return the current FastMCP request context details (client_id, session_id, and meta dump)."
---

# `debug_request_context`

> **Auto-generated** from the Python tool registry. Do not hand-edit outside `<!-- examples:start --><!-- examples:end -->` blocks — the generator (`tools/generate_docs_reference.py`) will overwrite them.

**Group:** `core` &nbsp;·&nbsp; **Module:** `services.tools.debug_request_context`

## Description

Return the current FastMCP request context details (client_id, session_id, and meta dump).

## Parameters

_No parameters._

## Returns

A `dict` containing the Unity response. The exact shape depends on the action.

## Examples

<!-- examples:start -->
Inspect request/session routing when commands reach the wrong project.
**Prerequisite:** an MCP client; no arguments are required. Call `debug_request_context`:

```json
{}
```

**Verify:** inspect the returned request/session/instance context. Check for private
project/session information before sharing diagnostic output.
<!-- examples:end -->
