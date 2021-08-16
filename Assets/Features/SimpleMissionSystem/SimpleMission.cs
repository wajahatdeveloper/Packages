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
    COLLECT
}

public class SimpleMission : MonoBehaviour
{
    public string missionName;
    [Multiline] public string missionStatement;
    public MissionType missionType;
    public StartPoint startPoint;

    #region TRAVEL_DECL

    [ConditionalField(nameof(missionType), false, MissionType.TRAVEL)]
    public Destination destination;
    
    #endregion

    #region DESTROY_DECL

    [ConditionalField(nameof(missionType), false, MissionType.DESTROY)]
    public int destroyCount = 0;

    private int _destroyedKeys = 0;
    
    #endregion
    
    #region COLLECT_DECL

    [ConditionalField(nameof(missionType), false, MissionType.COLLECT)]
    public int collectCount = 0;

    private int _collectedKeys = 0;
    
    #endregion

    [Space]
    
    public bool startOnEnable = true;
    public bool disableOnComplete = true;

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
        SimpleMissionManager.instance.CurrentMission = this;
        
        SimpleMissionManager.instance.missionList.AddIfNotExists(missionName,this);
        
        startPoint.MissionStart();
        
        OnMissionStart?.Invoke();
        
        switch (missionType)
        {
            case MissionType.TRAVEL:
                Mission_Travel_Start();
                break;
            case MissionType.DESTROY:
                Mission_Destroy_Start();
                break;
            case MissionType.COLLECT:
                Mission_Collect_Start();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        ConsoleProDebug.LogToFilter($"Mission {missionName} Started : Type => {missionType}","Missions");
    }

    public void EndMission()
    {
        OnMissionFinished?.Invoke();
        if (disableOnComplete) { gameObject.SetActive(false); }
    }
    
    #region TRAVEL

    private void Mission_Travel_Start()
    {
    }

    public void Mission_Travel_DestinationReached()
    {
        Mission_Travel_Complete();
    }

    private void Mission_Travel_Complete()
    {
        EndMission();
    }

    #endregion
    
    #region DESTROY

    private void Mission_Destroy_Start()
    {
    }

    public void Mission_Destroy_KeyDestroyed()
    {
        _destroyedKeys++;
        if (_destroyedKeys >= destroyCount)
        {
            Mission_Destroy_Complete();
        }
    }

    private void Mission_Destroy_Complete()
    {
        EndMission();
    }

    #endregion
    
    #region COLLECT

    private void Mission_Collect_Start()
    {
    }

    public void Mission_Collect_KeyCollected()
    {
        _collectedKeys++;
        if (_collectedKeys >= collectCount)
        {
            Mission_Collect_Complete();
        }
    }

    private void Mission_Collect_Complete()
    {
        EndMission();
    }

    #endregion
}