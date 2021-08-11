using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : RuleBehaviour
{
    public Model_Variables dataModelVariables;

    private void Start()
    {
        // ------------------- Rule 1 -------------------
        // Get data from model
        float sceneChangeDelay = dataModelVariables.floatVars[0];
        
        // Load next scene after delay
        this.Invoke(SceneManagerX.LoadNextScene,sceneChangeDelay);

        // ------------------- Rule 2 -------------------
        // ..
    }
}