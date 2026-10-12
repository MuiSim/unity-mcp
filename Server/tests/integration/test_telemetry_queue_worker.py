import logging
import queue
import threading
import time
import types
from unittest.mock import patch

import pytest

import core.telemetry as telemetry


def test_telemetry_queue_backpressure_and_single_worker(tmp_path, caplog):
    logger = logging.getLogger("unity-mcp-telemetry")
    logger.addHandler(caplog.handler)
    with patch("core.telemetry.TelemetryConfig._get_data_directory", return_value=tmp_path):
        collector = telemetry.TelemetryCollector()
    try:
        caplog.set_level("DEBUG", logger="unity-mcp-telemetry")
        collector.config.enabled = True
        collector.record(telemetry.RecordType.TOOL_EXECUTION, {"i": -1})
        collector._queue = queue.Queue(maxsize=2)
        time.sleep(0.2)

        def slow_send(self, record):
            time.sleep(0.05)

        collector._send_telemetry = types.MethodType(slow_send, collector)
        start = time.perf_counter()
        for i in range(50):
            collector.record(telemetry.RecordType.TOOL_EXECUTION, {"i": i})
        elapsed_ms = (time.perf_counter() - start) * 1000

        assert elapsed_ms < 500
        assert any("Telemetry queue full; dropping" in message for message in caplog.messages)
        assert collector._worker.is_alive()
        assert sum(thread is collector._worker for thread in threading.enumerate()) == 1
    finally:
        collector.shutdown()
        logger.removeHandler(caplog.handler)


@pytest.mark.parametrize("endpoint", [
    "https://api-prod.coplay.dev/telemetry/events",
    "https://owner.example/telemetry",
])
def test_local_collection_never_reports_to_remote_server(tmp_path, monkeypatch, endpoint):
    for name in ("DISABLE_TELEMETRY", "UNITY_MCP_DISABLE_TELEMETRY", "MCP_DISABLE_TELEMETRY"):
        monkeypatch.delenv(name, raising=False)
    monkeypatch.setenv("UNITY_MCP_TELEMETRY_ENDPOINT", endpoint)
    telemetry.reset_telemetry()

    with (
        patch("core.telemetry.TelemetryConfig._get_data_directory", return_value=tmp_path),
        patch("httpx.Client") as client,
        patch("urllib.request.urlopen") as urlopen,
    ):
        collector = telemetry.get_telemetry()
        try:
            assert telemetry.is_telemetry_enabled() is True
            assert collector._worker.is_alive()
            assert collector.config.uuid_file.exists()
            assert telemetry.record_milestone(telemetry.MilestoneType.FIRST_STARTUP) is True
            assert collector.config.milestones_file.exists()

            telemetry.record_telemetry(telemetry.RecordType.STARTUP, {})
            telemetry.record_tool_usage("test", True, 1)
            telemetry.record_resource_usage("test", True, 1)
            telemetry.record_latency("test", 1)
            telemetry.record_failure("test", "error")
            collector._queue.join()

            collector._send_telemetry(telemetry.TelemetryRecord(
                telemetry.RecordType.STARTUP, 0, "test-uuid", "test-session", {}
            ))
            client.assert_not_called()
            urlopen.assert_not_called()
        finally:
            telemetry.reset_telemetry()
