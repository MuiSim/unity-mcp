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
                Assert.That(source, Does.StartWith("git+https://github.com/MuiSim/unity-mcp@"));
                Assert.That(source, Does.EndWith("#subdirectory=Server"));
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
    }
}
