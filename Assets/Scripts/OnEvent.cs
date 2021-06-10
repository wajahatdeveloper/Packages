using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEvent : MonoBehaviour
{
    public string eventIdentifier;

    private void OnEnable()
    {
        if (eventIdentifier.IsNullOrEmpty())
        {
            Debug.LogError($"{name} is missing Event Receive Identifier");
        }
        gameObject.ConnectEvent(eventIdentifier,(sender,eventData)=>OnEventReceived?.Invoke());
    }

    private void OnDisable()
    {
        gameObject.DisconnectEvent(eventIdentifier);
    }

    public UnityEvent OnEventReceived;
}