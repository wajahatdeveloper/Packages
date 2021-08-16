using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationView : MonoBehaviour
{
    [Header("Login UI")]
    public GameObject loginPanel;
    public Button showLoginPanelButton;
    public Button doLoginButton;
    public InputField loginIdInputField;
    public InputField loginPasswordInputField;
    
    [Header("Signup UI")]
    public GameObject signupPanel;
    public Button showSignupPanelButton;
    public Button doSignupButton;
    public InputField signupIdInputField;
    public InputField signupPasswordInputField;
    public InputField signupPasswordConfirmInputField;
}