using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key_Destroy : MonoBehaviour
{
    public string missionName;
    
    private SimpleMission _thisMission;

    private void Start()
    {
        _thisMission = SimpleMission.missionList[missionName];
    }

    private void OnDestroy()
    {
        _thisMission.Mission_Destroy_KeyDestroyed();
        ConsoleProDebug.LogToFilter($"Mission {_thisMission.name} Key Destroyed : Type => {_thisMission.missionType}","Missions");
    }
}