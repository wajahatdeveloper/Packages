using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMissionManager : SingletonBehaviour<SimpleMissionManager>
{
    private Transform _currentPlayerInstance = null;
    private SimpleMission _currentMission = null;

    public Transform CurrentPlayerInstance
    {
        get => _currentPlayerInstance;
        set
        {
            ConsoleProDebug.LogToFilter(
                value == null
                    ? $"Simple Missions : Player Instance Set To => Null"
                    : $"Simple Missions : Player Instance Set To => {value.name}", "Missions");
            _currentPlayerInstance = value;
        }
    }
    public SimpleMission CurrentMission
    {
        get => _currentMission;
        set
        {
            ConsoleProDebug.LogToFilter(
                value == null
                    ? $"Simple Missions : Current Mission Set To => Null"
                    : $"Simple Missions : Current Mission Set To => {value.name}", "Missions");
            _currentMission = value;
        }
    }

    public Dictionary<string, SimpleMission> missionList;
}