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

    private bool _isAuto = false;

    public void ShowAuto(string message)
    {
        _isAuto = true;
        Show(message);
    }
    
    public void Show(string message)
    {
        messageText.text = message;
        messagePanel.SetActive(true);
    }

    public void HideAuto()
    {
        _isAuto = false;
        Hide();
    }
    
    public void Hide()
    {
        if (_isAuto) { return; }
        messagePanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}