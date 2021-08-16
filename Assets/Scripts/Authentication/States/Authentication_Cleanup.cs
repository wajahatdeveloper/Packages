using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;

public class Authentication_Cleanup : BState
{
    private AuthenticationView _view = AuthenticationRoot.instance.view;

    public override void Enter()
    {
        base.Enter();
        // Rule: Hide all authentication panels on cleanup
        _view.loginPanel.SetActive(false);
        _view.signupPanel.SetActive(false);
        _view.showLoginPanelButton.SetActive(false);
        _view.showSignupPanelButton.SetActive(false);
    }
}