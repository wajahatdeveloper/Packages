using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputBox : SingletonBehaviour<InputBox>
{
    public Text headingText;
    public InputField inputField;
    public GameObject inputPanel;

    public string enteredText = ""; 

    public UnityEvent<string> onClose;

    public void Show(string message)
    {
        headingText.text = message;
        inputPanel.SetActive(true);
    }

    public void OnClick_Submit()
    {
        enteredText = inputField.text;
        Hide();
    }

    public void Hide()
    {
        inputPanel.SetActive(false);
        onClose?.Invoke(enteredText);
        onClose?.RemoveAllListeners();
    }
}
