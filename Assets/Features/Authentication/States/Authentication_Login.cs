using System;
using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;
using UnityFx.Async;            // Library core.
using UnityFx.Async.Extensions; // BCL/Unity extension methods.
using UnityFx.Async.Promises;   // Promise extensions.
using MEC;
using Sirenix.OdinInspector;
using UnityEngine.Networking;

public class Authentication_Login : MonoRuleState
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
        // Rule: show login panel when in login state
        _view.loginPanel.SetActive(true);
    }

    public void OnClick_ShowSignupPanel()
    {
        AuthenticationRoot.Instance.controller.ChangeState<Authentication_Signup>();
    }
    
    public void OnSubmit_LoginId(string text)
    {
    }
    
    public void OnSubmit_LoginPassword(string text)
    {
    }

    public override void Exit()
    {
        base.Exit();
        // Rule: hide login panel when leaving login state
        _view.loginPanel.SetActive(false);
    }

    public void OnClick_DoLogin()
    {
        DoLoginApiCall(_model.loginApiUrl)
            .Then(text =>
            {
                Login_Success();
                Debug.Log(text);
            })
            .Catch(e =>
            {
                Login_Failed();
                Debug.LogException(e);
            });
    }

    public IAsyncOperation<string> DoLoginApiCall(string url)
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

    private void Login_Success()
    {
        // Show Message Success
    }

    private void Login_Failed()
    {
        // Show Message Failed
    }
}