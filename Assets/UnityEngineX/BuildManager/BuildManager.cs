using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngineX
{
    public class BuildManager : MonoBehaviour
    {
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