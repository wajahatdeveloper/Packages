using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int targetFrameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Debug.Log($"Time Scale Set To {timeScale}");
    }
}