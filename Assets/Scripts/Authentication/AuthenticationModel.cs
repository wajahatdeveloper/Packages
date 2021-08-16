using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticationModel : MonoBehaviour
{
    [Header("Login")]
    public string loginId;
    public string loginPassword;
    public string loginApiUrl;

    [Header("Signup")]
    public string signupId;
    public string signupPassword;
    public string signupPasswordConfirm;
    public string signupApiUrl;
}