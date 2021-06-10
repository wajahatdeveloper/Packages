using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineX;

public class RaiseEvent : MonoBehaviour
{
    public bool autoHookButton = true;
    //[ConditionalField(nameof(autoHookButton))]
    public string eventIdentifier;

    private void Start()
    {
        if (autoHookButton)
        {
            GetComponent<Button>()?.onClick.AddListener(RaiseEventDefault);
        }
    }

    private void RaiseEventDefault()
    {
        gameObject.RaiseEvent(eventIdentifier);
    }
    
    public void RaiseGameObjectEvent(string evtId)
    {
        gameObject.RaiseEvent(evtId);
    }
}