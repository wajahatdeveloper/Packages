using UnityEngine;
using UnityEngine.Events;

public class WaitPanel : SingletonBehaviour<WaitPanel>
{
    public GameObject waitPanel;
    public UnityEvent onClose;

    private int _count = 0;
    
    public void Show()
    {
        if (waitPanel == null)
        {
            Debug.LogError("Wait panel game object not assigned");
        }
        else
        {
            Debug.Log("Wait Panel Shown");
            waitPanel.SetActive(true);
        }
    }
    
    public void ShowCounted()
    {
        if (waitPanel == null)
        {
            Debug.LogError("Wait panel game object not assigned");
        }
        else
        {
            _count++;
            Debug.Log("Wait Panel Shown : count = " + _count);
            waitPanel.SetActive(true);
        }
    }

    public void HideCounted()
    {
        _count--;
        if (_count <= 0)
        {
            Hide();
        }
    }
    
    public void Hide()
    {
        Debug.Log("Wait Panel Hidden");
        waitPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}