using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Destination : MonoBehaviour
{
    public string missionName;
    [Tag] public string tagToCheck = "";
    public UnityEvent OnDestinationReached;
    
    private SimpleMission _thisMission;

    private void Start()
    {
        _thisMission = SimpleMissionManager.instance.missionList[missionName];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(tagToCheck)) { return; }
        OnDestinationReached?.Invoke();
        _thisMission.Mission_Travel_DestinationReached();
        ConsoleProDebug.LogToFilter($"Mission {_thisMission.name} Destination Reached : Type => {_thisMission.missionType}","Missions");
    }
}