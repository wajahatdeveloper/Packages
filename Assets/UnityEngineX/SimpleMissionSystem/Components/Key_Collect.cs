using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Collect : MonoBehaviour 
{
    public string missionName;
    
    private SimpleMission _thisMission;

    private void Start()
    {
        _thisMission = SimpleMission.missionList[missionName];
    }

    private void OnDestroy()
    {
        _thisMission.Mission_Collect_KeyCollected();
        ConsoleProDebug.LogToFilter($"Mission {_thisMission.name} Key Collected : Type => {_thisMission.missionType}","Missions");
    }
}