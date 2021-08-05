using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat 
{
    public void Repeat_PlayerPref(int count, Action action)
    {
    }
    
    public void Repeat_Static(int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action?.Invoke();
        }
    }
    
    public void Repeat_Count(int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action?.Invoke();
        }
    }
}