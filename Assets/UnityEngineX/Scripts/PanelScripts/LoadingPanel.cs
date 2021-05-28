using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngineX;

public class LoadingPanel : SingletonBehaviour<LoadingPanel>
{
    public GameObject loadingPanel;

    public UnityEvent onClose;

    public void Show()
    {
        loadingPanel.SetActive(true);
    }

    public void Hide()
    {
        loadingPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}