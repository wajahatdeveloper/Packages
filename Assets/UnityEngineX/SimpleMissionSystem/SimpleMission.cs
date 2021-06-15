using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public enum MissionType
{
    TRAVEL,
    DESTROY,
    COLLECT,
    ESCORT,
    SEARCH,
    REPAIR,
    DELIVERY
}

public class SimpleMission : MonoBehaviour
{
    public static SimpleMission CurrentMission = null;
    
    public string missionName;
    [Multiline] public string missionStatement;
    public bool startOnEnable = true;
    public MissionType missionType;
    public StartPoint startPoint;

    [ConditionalField(nameof(missionType), false, MissionType.TRAVEL)]
    public Destination destination;

    private void OnEnable()
    {
        if (startOnEnable) { StartMission(); }
    }

    public void StartMission()
    {
        CurrentMission = this;
        
        destination.thisMission = this;

        switch (missionType)
        {
            case MissionType.TRAVEL:
                Mission_Travel_Start();
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
        ConsoleProDebug.LogToFilter($"Mission {missionName} Started : Type => {missionType}","Missions");
    }

    private void Mission_Travel_Start()
    {
    }
}