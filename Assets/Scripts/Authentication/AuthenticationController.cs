using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticationController : RuleBehaviourFSM
{
    public AuthenticationModel model;

    public override void AddStates()
    {
        // set custom update frequency
        SetUpdateFrequency(0.1f);
        
        // add the states
        AddState<Authentication_Initialize>();
        AddState<Authentication_Login>();
        AddState<Authentication_Signup>();
        AddState<Authentication_Cleanup>();
        
        // Rule: Set Initial Authentication State
        SetInitialState<Authentication_Initialize>();
    }
}