using UnityEngine;
using UnityEngine.Events;

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