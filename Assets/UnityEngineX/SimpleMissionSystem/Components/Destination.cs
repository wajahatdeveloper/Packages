using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Destination : MonoBehaviour
{
    [Tag] public string tagToCheck = "";
    public UnityEvent OnDestinationReached;
    
    [HideInInspector] public SimpleMission thisMission;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(tagToCheck)) { return; }

        switch (thisMission.missionType)
        {
            case MissionType.TRAVEL:
                thisMission.OnMissionFinished?.Invoke();
                break;
            case MissionType.DESTROY:
                break;
            case MissionType.COLLECT:
                break;
            case MissionType.ESCORT:
                break;
            case MissionType.SEARCH:
                break;
            case MissionType.REPAIR:
                break;
            case MissionType.DELIVERY:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        OnDestinationReached?.Invoke();
        ConsoleProDebug.LogToFilter($"Mission {thisMission.name} Destination Reached : Type => {thisMission.missionType}","Missions");
    }
}