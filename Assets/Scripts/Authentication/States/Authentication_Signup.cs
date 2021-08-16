using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;

public class Authentication_Signup : BState
{
    private AuthenticationView _view = AuthenticationRoot.instance.view;
    private AuthenticationModel _model = AuthenticationRoot.instance.controller.model;

    public override void Enter()
    {
        base.Enter();
        // Rule: show signup panel when in signup state
        _view.signupPanel.SetActive(true);
        _view.showSignupPanelButton.SetActive(false);
        _view.doSignupButton.onClick.AddListener(OnClick_DoSignup);
        _view.loginIdInputField.onEndEdit.AddListener(OnSubmit_SignupId);
        _view.loginIdInputField.onEndEdit.AddListener(OnSubmit_SignupPassword);
        _view.loginIdInputField.onEndEdit.AddListener(OnSubmit_SignupPasswordConfirm);
    }

    private void OnSubmit_SignupPasswordConfirm(string text)
    {
        if (_model.signupPassword.Equals(_model.signupPasswordConfirm))
        {
            // Validation Success
        }
        else
        {
            // Validation Failed
        }
    }

    private void OnSubmit_SignupPassword(string text)
    {
    }

    private void OnSubmit_SignupId(string text)
    {
    }

    public override void Exit()
    {
        base.Exit();
        // Rule: hide signup panel when leaving signup state
        _view.signupPanel.SetActive(false);
        _view.showSignupPanelButton.SetActive(true);
        _view.doSignupButton.onClick.RemoveListener(OnClick_DoSignup);
        _view.loginIdInputField.onEndEdit.RemoveListener(OnSubmit_SignupId);
        _view.loginIdInputField.onEndEdit.RemoveListener(OnSubmit_SignupPassword);
        _view.loginIdInputField.onEndEdit.RemoveListener(OnSubmit_SignupPasswordConfirm);
    }
    
    private void OnClick_DoSignup()
    {
        bool success = false;
        // API Call Here

        if (success)
        {
            Signup_Success();
        }
        else
        {
            Signup_Failed();
        }
    }

    private void Signup_Success()
    {
        // Show Message Success
    }

    private void Signup_Failed()
    {
        // Show Message Failed
    }
}