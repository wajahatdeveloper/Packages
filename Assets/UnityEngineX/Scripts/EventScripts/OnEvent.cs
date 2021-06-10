using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class OnEvent : MonoBehaviour
{
    public string eventIdentifier;
    public UnityEvent OnEventReceived;
    public UnityEvent<string> OnEventReceived_String;

    private void OnEnable()
    {
        if (eventIdentifier.IsNullOrEmpty())
        {
            Debug.LogError($"{name} is missing Event Receive Identifier");
        }
        gameObject.ConnectEvent(eventIdentifier,EventCall);
    }

    private void EventCall(GameObject sender, object eventData)
    {
        if (eventData.IsNull()) { OnEventReceived?.Invoke(); }
        string str = eventData as string;
        OnEventReceived_String?.Invoke(str);
    }

    private void OnDisable()
    {
        gameObject.DisconnectEvent(eventIdentifier);
    }
}