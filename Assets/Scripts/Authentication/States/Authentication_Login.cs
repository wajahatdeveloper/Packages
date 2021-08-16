using System;
using System.Collections;
using System.Collections.Generic;
using RobustFSM.Base;
using UnityEngine;
using UnityFx.Async;            // Library core.
using UnityFx.Async.Extensions; // BCL/Unity extension methods.
using UnityFx.Async.Promises;   // Promise extensions.
using MEC;
using UnityEngine.Networking;

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