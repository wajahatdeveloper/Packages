using System;
using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using Sirenix.OdinInspector;
using UnityEngine;

public class Authentication_Cleanup : MonoRuleState
{
    private AuthenticationView _view;
    private AuthenticationModel _model;

    private void InitiaizeReferences()
    {
        _view = AuthenticationRoot.Instance.view;
        _model = AuthenticationRoot.Instance.controller.model;
    }

    public override void Enter()
    {
        InitiaizeReferences();
        base.Enter();
        // Rule: Hide all authentication panels on cleanup
        _view.loginPanel.SetActive(false);
        _view.signupPanel.SetActive(false);
    }
}