using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : RuleBehaviour 
{
    private void Start()
    {
        SceneManagerX.LoadNextScene();
    }
}