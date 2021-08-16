using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerX
{
    public static void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 2 > SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Scene Manager: No Next Scene Available to Load");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    public static void LoadPreviousScene()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 < 0)
        {
            Debug.LogWarning("Scene Manager: No Previous Scene Available to Load");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}