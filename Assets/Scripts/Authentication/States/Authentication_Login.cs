using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;

public class Authentication_Login : BState
{
    private AuthenticationView _view = AuthenticationRoot.instance.view;
    private AuthenticationModel _model = AuthenticationRoot.instance.controller.model;

    public override void Enter()
    {
        base.Enter();
        // Rule: show login panel when in login state
        _view.loginPanel.SetActive(true);
        _view.showLoginPanelButton.SetActive(false);
        _view.doLoginButton.onClick.AddListener(OnClick_DoLogin);
        _view.loginIdInputField.onEndEdit.AddListener(OnSubmit_LoginId);
        _view.loginIdInputField.onEndEdit.AddListener(OnSubmit_LoginPassword);
    }

    private void OnSubmit_LoginId(string text)
    {
    }
    
    private void OnSubmit_LoginPassword(string text)
    {
    }

    public override void Exit()
    {
        base.Exit();
        // Rule: hide login panel when leaving login state
        _view.loginPanel.SetActive(false);
        _view.showLoginPanelButton.SetActive(true);
        _view.doLoginButton.onClick.RemoveListener(OnClick_DoLogin);
        _view.loginIdInputField.onEndEdit.RemoveListener(OnSubmit_LoginId);
        _view.loginIdInputField.onEndEdit.RemoveListener(OnSubmit_LoginPassword);
    }

    private void OnClick_DoLogin()
    {
        bool success = false;
        // API Call Here

        if (success)
        {
            Login_Success();
        }
        else
        {
            Login_Failed();
        }
    }

    private void Login_Success()
    {
        // Show Message Success
    }

    private void Login_Failed()
    {
        // Show Message Failed
    }
}