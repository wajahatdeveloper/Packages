using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace UnityEngineX
{
    [HideMonoScript]
    public class BuildVersioning : MonoBehaviour
    {
#if UNITY_EDITOR
        [HorizontalGroup("Group"),HideLabel] public int majorVersion;
        [HorizontalGroup("Group"),HideLabel] public int minorVersion;
        [HorizontalGroup("Group"),HideLabel] public int revisionVersion;
        [HorizontalGroup("Group"),HideLabel] public int buildVersion;

        private void OnValidate()
        {
            if (majorVersion == 0 && minorVersion == 0 && revisionVersion == 0 && buildVersion == 0) { return; }
            var bundleVersion = $"{majorVersion}.{minorVersion}.{revisionVersion}.{buildVersion}";
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