using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;
using UnityEngine.UI;

public class Authentication_Initialize : BState
{
    private AuthenticationView _view = AuthenticationRoot.instance.view;
    
    public override void Enter()
    {
        base.Enter();
        // Rule: By default show login panel at start
        _view.loginPanel.SetActive(true);
        // Rule: Hook show login panel button
        var showLoginPanelButton = _view.showLoginPanelButton.GetComponent<Button>();
        showLoginPanelButton.onClick.AddListener(OnClick_ShowLoginPanel);
        // Rule: Hook show signup panel button
        var showSignupPanelButton = _view.showSignupPanelButton.GetComponent<Button>();
        showSignupPanelButton.onClick.AddListener(OnClick_ShowSignupPanel);
    }

    public void OnClick_ShowLoginPanel()
    {
        AuthenticationRoot.instance.controller.SetCurrentState<Authentication_Login>();
    }

    public void OnClick_ShowSignupPanel()
    {
        AuthenticationRoot.instance.controller.SetCurrentState<Authentication_Signup>();
    }
}