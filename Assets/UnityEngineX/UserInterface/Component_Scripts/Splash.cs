using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : SingletonBehaviour<Splash>
{
    public float delayTillNextScene = 1.0f;
    public int nextSceneIndex = 1;

    private void Start()
    {
        this.Invoke(()=>SceneManager.LoadScene(nextSceneIndex),delayTillNextScene);
    }
}