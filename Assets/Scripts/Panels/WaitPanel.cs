using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngineX;

public class WaitPanel : SingletonBehaviour<WaitPanel>
{
    public GameObject waitPanel;

    public UnityEvent onClose;

    public void Show()
    {
        waitPanel.SetActive(true);
    }

    public void Hide()
    {
        waitPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}