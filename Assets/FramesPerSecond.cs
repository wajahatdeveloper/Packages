using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesPerSecond : MonoBehaviour 
{
    public int targetFramesPerSecond = 30;
    
    private void Start()
    {
        Application.targetFrameRate = targetFramesPerSecond;
        Debug.Log("Target Frame Rate = " + targetFramesPerSecond);
    }
}