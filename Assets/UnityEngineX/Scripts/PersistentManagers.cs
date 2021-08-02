using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManagers : MonoBehaviour 
{
    private void Start()
    {
        int debugSceneIdx = 2;
        if (PlayerPrefs.HasKey("DebugScene") == false)
        {
            PlayerPrefs.SetInt("DebugScene",2);
        }
        else
        {
            debugSceneIdx = PlayerPrefs.GetInt("DebugScene", 2);
        }
        SceneManager.LoadScene(debugSceneIdx);
    }
}