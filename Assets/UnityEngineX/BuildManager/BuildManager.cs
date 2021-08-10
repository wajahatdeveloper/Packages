using System;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace UnityEngineX
{
    public class BuildManager : MonoBehaviour
    {
#if UNITY_EDITOR
        public Vector4 buildNumber = Vector4.zero;

        private void OnValidate()
        {
            if (buildNumber == Vector4.zero) return;
            var bundleVersion = $"{buildNumber.x}.{buildNumber.y}.{buildNumber.z}.{buildNumber.w}";
            if (PlayerSettings.bundleVersion == bundleVersion) return;
            PlayerSettings.bundleVersion = bundleVersion;
            Debug.Log("Version Changed to " + PlayerSettings.bundleVersion);
        }
#endif

        public string StoredBuildVersion
        {
            set => PlayerPrefs.SetString("buildVersion", value);
            get => PlayerPrefs.GetString("buildVersion", "");
        }

        private void OnEnable()
        {
            string storedVersion = StoredBuildVersion;
            if (Application.version != storedVersion)
            {
                PlayerPrefs.DeleteAll();
                print("Build Version Mismatch: Cleared Previous Player Preferences Form Build");
                StoredBuildVersion = Application.version;
            }
        }
    }
}