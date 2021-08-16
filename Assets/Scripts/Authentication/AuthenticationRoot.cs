using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AuthenticationRoot : SingletonBehaviour<AuthenticationRoot>
{
    [Required]
    public AuthenticationController controller;
    [Required]
    public AuthenticationView view;
}