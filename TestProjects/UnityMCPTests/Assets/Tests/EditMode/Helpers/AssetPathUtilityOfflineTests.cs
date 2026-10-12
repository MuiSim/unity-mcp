using NUnit.Framework;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Constants;
using MCPForUnity.Editor.Setup;
using System.Reflection;
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

        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#v10.3.0", "10.3.0", "v10.3.0")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#beta", "10.3.0", "beta")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#main", "10.3.0", "main")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#abc123", "10.3.0", "abc123")]
        [TestCase("com.coplaydev.unity-mcp@file:../../../MCPForUnity", "10.3.0", "v10.3.0")]
        [TestCase(null, "10.3.0-beta.6", "v10.3.0-beta.6")]
        [TestCase(null, "unknown", "muisim-stable-10-3-0-release")]
        [TestCase(null, null, "muisim-stable-10-3-0-release")]
        [TestCase(null, "", "muisim-stable-10-3-0-release")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#muisim-stable-10-3-0-release", "10.3.0", "muisim-stable-10-3-0-release")]
        [TestCase("com.coplaydev.unity-mcp@https://github.com/MuiSim/unity-mcp.git#", "10.3.0", "v10.3.0")]
        public void GetMcpServerPackageSource_MatchesInstalledRefOrVersion(string packageId, string version, string expectedRef)
        {
            Assert.AreEqual(
                $"https://github.com/MuiSim/unity-mcp/archive/{expectedRef}.zip#subdirectory=Server",
                AssetPathUtility.GetMcpServerPackageSource(packageId, version));
        }

        [Test]
        public void SkillInstaller_DefaultsToStableForkTag()
        {
            var type = typeof(McpForUnitySkillInstaller);
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
            string repoKey = (string)type.GetField("RepoUrlKey", flags).GetRawConstantValue();
            string branchKey = (string)type.GetField("BranchKey", flags).GetRawConstantValue();
            bool hadRepo = EditorPrefs.HasKey(repoKey);
            bool hadBranch = EditorPrefs.HasKey(branchKey);
            string oldRepo = EditorPrefs.GetString(repoKey, "");
            string oldBranch = EditorPrefs.GetString(branchKey, "");
            McpForUnitySkillInstaller installer = null;
            try
            {
                EditorPrefs.DeleteKey(repoKey);
                EditorPrefs.DeleteKey(branchKey);
                installer = UnityEngine.ScriptableObject.CreateInstance<McpForUnitySkillInstaller>();
                const BindingFlags instanceFlags = BindingFlags.NonPublic | BindingFlags.Instance;
                Assert.AreEqual("https://github.com/MuiSim/unity-mcp",
                    type.GetField("_repoUrl", instanceFlags).GetValue(installer));
                Assert.AreEqual("v10.3.0",
                    type.GetField("_targetBranch", instanceFlags).GetValue(installer));
            }
            finally
            {
                if (installer != null) UnityEngine.Object.DestroyImmediate(installer);
                if (hadRepo) EditorPrefs.SetString(repoKey, oldRepo);
                else EditorPrefs.DeleteKey(repoKey);
                if (hadBranch) EditorPrefs.SetString(branchKey, oldBranch);
                else EditorPrefs.DeleteKey(branchKey);
            }
        }
    }
}
