using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngineX;

public class PopupMessage : SingletonBehaviour<PopupMessage>
{
    public Text messageText;
    public GameObject messagePanel;

    public UnityEvent onClose;

    public void Show(string message)
    {
        messageText.text = message;
        messagePanel.SetActive(true);
    }

    public void Hide()
    {
        messagePanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}