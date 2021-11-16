using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Root<M,V,C> : SingletonBehaviour<Root<M,V,C>>
    where M : Model
    where V : View
    where C : Controller<M,V>
{
    [Required] public M Model;
    [Required] public C Controller;
    [Required] public V View;

    [HideLabel, TextArea]
    public string Description;

    private void OnEnable()
    {
        Debug.Log("Entered Gameplay Scene");
        
        InjectMockData();
    }

    private void OnDisable()
    {
        Debug.Log("Left Gameplay Scene");
    }

    protected virtual void InjectMockData()
    {
        Debug.Log("Mock Data Injected");
    }
}