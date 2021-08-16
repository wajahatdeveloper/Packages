using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;
using UnityEngine.UI;

public class Authentication_Initialize : MonoRuleState
{
    private AuthenticationView _view;
    private AuthenticationModel _model;

    private void InitiaizeReferences()
    {
        _view = AuthenticationRoot.instance.view;
        _model = AuthenticationRoot.instance.controller.model;
    }
    
    public override void Enter()
    {
        InitiaizeReferences();
        base.Enter();
        // Rule: By default show login panel at start
        AuthenticationRoot.instance.controller.ChangeState<Authentication_Login>();
    }
}