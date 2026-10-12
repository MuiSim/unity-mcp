using System;
using System.Collections.Generic;
using MCPForUnity.Editor.Constants;
using MCPForUnity.Editor.Helpers;
using NUnit.Framework;
using UnityEditor;

namespace MCPForUnityTests.Editor.Helpers
{
    public class TelemetryHelperTests
    {
        [Test]
        public void TelemetryCanBeCreatedButCannotBeReported()
        {
            bool hadPreference = EditorPrefs.HasKey(EditorPrefKeys.TelemetryDisabled);
            bool oldPreference = EditorPrefs.GetBool(EditorPrefKeys.TelemetryDisabled);
            string oldUuid = EditorPrefs.GetString(EditorPrefKeys.CustomerUuid, "");
            bool hadUuid = EditorPrefs.HasKey(EditorPrefKeys.CustomerUuid);
            string[] disableVariables = { "DISABLE_TELEMETRY", "UNITY_MCP_DISABLE_TELEMETRY", "MCP_DISABLE_TELEMETRY" };
            var oldEnvironment = new Dictionary<string, string>();
            bool sent = false;
            try
            {
                foreach (string name in disableVariables)
                {
                    oldEnvironment[name] = Environment.GetEnvironmentVariable(name);
                    Environment.SetEnvironmentVariable(name, null);
                }
                EditorPrefs.SetBool(EditorPrefKeys.TelemetryDisabled, false);
                EditorPrefs.DeleteKey(EditorPrefKeys.CustomerUuid);
                TelemetryHelper.RegisterTelemetrySender(_ => sent = true);

                Assert.IsTrue(TelemetryHelper.IsEnabled);
                Assert.IsFalse(TelemetryHelper.IsReportingEnabled);
                TelemetryHelper.RecordEvent("test", new Dictionary<string, object>());
                TelemetryHelper.RecordBridgeStartup();
                TelemetryHelper.RecordBridgeConnection(true);
                TelemetryHelper.RecordToolExecution("test", true, 1);

                Assert.IsFalse(sent);
                Assert.IsTrue(EditorPrefs.HasKey(EditorPrefKeys.CustomerUuid));
            }
            finally
            {
                TelemetryHelper.UnregisterTelemetrySender();
                foreach (var entry in oldEnvironment)
                    Environment.SetEnvironmentVariable(entry.Key, entry.Value);
                if (hadPreference) EditorPrefs.SetBool(EditorPrefKeys.TelemetryDisabled, oldPreference);
                else EditorPrefs.DeleteKey(EditorPrefKeys.TelemetryDisabled);
                if (hadUuid) EditorPrefs.SetString(EditorPrefKeys.CustomerUuid, oldUuid);
                else EditorPrefs.DeleteKey(EditorPrefKeys.CustomerUuid);
            }
        }
    }
}
