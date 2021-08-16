using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticationRoot : SingletonBehaviour<AuthenticationRoot>
{
    public AuthenticationController controller;
    public AuthenticationView view;
}