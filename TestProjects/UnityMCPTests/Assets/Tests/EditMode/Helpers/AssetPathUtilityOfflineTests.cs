using NUnit.Framework;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Constants;
using UnityEditor;

namespace MCPForUnityTests.Editor.Helpers
{
    public class AssetPathUtilityOfflineTests
    {
        private bool _originalForceRefresh;

        [SetUp]
        public void SetUp()
        {
            _originalForceRefresh = EditorPrefs.GetBool(EditorPrefKeys.DevModeForceServerRefresh, false);
        }

        [TearDown]
        public void TearDown()
        {
            EditorPrefs.SetBool(EditorPrefKeys.DevModeForceServerRefresh, _originalForceRefresh);
        }

        [Test]
        public void ShouldUseUvxOffline_WhenForceRefreshEnabled_ReturnsFalse()
        {
            EditorPrefs.SetBool(EditorPrefKeys.DevModeForceServerRefresh, true);
            Assert.IsFalse(AssetPathUtility.ShouldUseUvxOffline());
        }

        [Test]
        public void ShouldUseUvxOffline_DoesNotThrow()
        {
            EditorPrefs.SetBool(EditorPrefKeys.DevModeForceServerRefresh, false);
            Assert.DoesNotThrow(() => AssetPathUtility.ShouldUseUvxOffline());
        }

        [Test]
        public void GetMcpServerPackageSource_DefaultsToFork()
        {
            bool hadOverride = EditorPrefs.HasKey(EditorPrefKeys.GitUrlOverride);
            string oldOverride = EditorPrefs.GetString(EditorPrefKeys.GitUrlOverride, "");
            try
            {
                EditorPrefs.DeleteKey(EditorPrefKeys.GitUrlOverride);
                string source = AssetPathUtility.GetMcpServerPackageSource();
                Assert.That(source, Does.StartWith("https://github.com/MuiSim/unity-mcp/archive/"));
                Assert.That(source, Does.EndWith(".zip#subdirectory=Server"));
                Assert.That(source, Does.Not.Contain("mcpforunityserver"));

                const string explicitSource = "git+https://github.com/owner/custom-server@main#subdirectory=Server";
                EditorPrefs.SetString(EditorPrefKeys.GitUrlOverride, explicitSource);
                Assert.AreEqual(explicitSource, AssetPathUtility.GetMcpServerPackageSource());
            }
            finally
            {
                if (hadOverride) EditorPrefs.SetString(EditorPrefKeys.GitUrlOverride, oldOverride);
                else EditorPrefs.DeleteKey(EditorPrefKeys.GitUrlOverride);
            }
        }

        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#v10.3.1", "10.3.1", "v10.3.1")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#beta", "10.3.1", "beta")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#main", "10.3.1", "main")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#abc123", "10.3.1", "abc123")]
        [TestCase("com.coplaydev.unity-mcp@file:../../../MCPForUnity", "10.3.1", "v10.3.1")]
        [TestCase(null, "10.3.1-beta.6", "v10.3.1-beta.6")]
        [TestCase(null, "unknown", "main")]
        [TestCase(null, null, "main")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git#", "10.3.1", "v10.3.1")]
        public void GetMcpServerPackageSource_MatchesInstalledRefOrVersion(string packageId, string version, string expectedRef)
        {
            Assert.AreEqual(
                $"https://github.com/MuiSim/unity-mcp/archive/{expectedRef}.zip#subdirectory=Server",
                AssetPathUtility.GetMcpServerPackageSource(packageId, version));
        }
    }
}
