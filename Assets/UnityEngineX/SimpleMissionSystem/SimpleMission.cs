using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

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

    private static Transform _currentPlayerInstance = null;
    
    public static Transform CurrentPlayerInstance
    {
        get => _currentPlayerInstance;
        set
        {
            if (value == null)
            {
                ConsoleProDebug.LogToFilter($"Simple Missions : Player Instance Set To => Null","Missions");
            }
            else
            {
                ConsoleProDebug.LogToFilter($"Simple Missions : Player Instance Set To => {value.name}","Missions");
            }
            _currentPlayerInstance = value;
        }
    }

    public string missionName;
    [Multiline] public string missionStatement;
    public MissionType missionType;
    public StartPoint startPoint;

    [ConditionalField(nameof(missionType), false, MissionType.TRAVEL)]
    public Destination destination;

    [Space]
    
    public bool startOnEnable = true;

    [Space]
    
    public bool useEvents = false;

    [ConditionalField(nameof(useEvents))] public UnityEvent OnMissionStart;
    [ConditionalField(nameof(useEvents))] public UnityEvent OnMissionFinished;

    private void OnEnable()
    {
        if (startOnEnable) { StartMission(); }
    }

    public void StartMission()
    {
        CurrentMission = this;
     
        startPoint.MissionStart();
        
        OnMissionStart?.Invoke();
        
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