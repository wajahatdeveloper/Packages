using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using RobustFSM.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using UnityFx.Async;
using UnityFx.Async.Promises;

public class Authentication_Signup : MonoRuleState
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
        // Rule: show signup panel when in signup state
        _view.signupPanel.SetActive(true);
    }

    public void OnClick_ShowLoginPanel()
    {
        AuthenticationRoot.Instance.controller.ChangeState<Authentication_Login>();
    }
    
    public void OnSubmit_SignupPasswordConfirm(string text)
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

    public void OnSubmit_SignupPassword(string text)
    {
    }

    public void OnSubmit_SignupId(string text)
    {
    }

    public override void Exit()
    {
        base.Exit();
        // Rule: hide signup panel when leaving signup state
        _view.signupPanel.SetActive(false);
    }
    
    public void OnClick_DoSignup()
    {
        DoSignupApiCall(_model.signupApiUrl)
            .Then(text =>
            {
                Signup_Success();
                Debug.Log(text);
            })
            .Catch(e =>
            {
                Signup_Failed();
                Debug.LogException(e);
            });
    }
    
    public IAsyncOperation<string> DoSignupApiCall(string url)
    {
        var result = new AsyncCompletionSource<string>();
        Timing.RunCoroutine(_InternalApiCall(url, result));
        return result;
    }

    private IEnumerator<float> _InternalApiCall(string url, IAsyncCompletionSource<string> op)
    {
        var www = UnityWebRequest.Get(url);
        yield return Timing.WaitUntilDone(www.SendWebRequest());

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            op.SetException(new Exception(www.error));
        }
        else
        {
            op.SetResult(www.downloadHandler.text);
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