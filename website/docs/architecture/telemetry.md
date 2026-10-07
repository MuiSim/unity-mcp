# MCP for Unity Telemetry

Remote telemetry reporting is disabled in the
[MuiSim/unity-mcp fork](https://github.com/MuiSim/unity-mcp).

The Python server and Unity bridge do not send usage analytics, startup events,
tool metrics, milestones, or diagnostic reports to remote telemetry servers.
Environment variables, server configuration, and Unity Editor preferences cannot
enable reporting. Event creation, the background event queue, and local UUID and
milestone storage remain available. Regular events are consumed without uploading
or saving their full payloads; this change does not add a local event-file exporter.

The existing telemetry opt-out environment variables and Unity Editor preferences
still control collection. `is_telemetry_enabled()` and Unity's
`TelemetryHelper.IsEnabled` describe collection, not remote reporting.

The collection infrastructure remains available for a future owner-controlled
server or local-file review implementation. No such destination is enabled now.

Normal local logging and communication between the MCP server and Unity Editor
remain available.

The documentation site's GoatCounter beacon is also disabled, including when
`GOATCOUNTER_CODE` is configured.

## Package update checks

Package update checks still make HTTPS requests, but only to this fork's
`MCPForUnity/package.json` on GitHub:

- Stable Git installations check the `main` branch.
- Beta Git installations check the `beta` branch.
- Asset Store installations check the fork's `main` branch instead of upstream
  Asset Store metadata.

Update results are cached separately from upstream results to avoid showing an
upstream version after switching to this fork.

Unity's default server source and skill-sync repository also point at this fork.
Tagged Unity installations select the same Python server tag, so a version pin
does not silently launch a server from a moving branch.
Existing explicit source and skill-repository overrides are preserved.

This policy applies to builds from this repository. An explicit server-source
override that installs an upstream package from PyPI or another repository does
not inherit these changes. Previously generated client configurations must be
regenerated to use the fork's default server source.
