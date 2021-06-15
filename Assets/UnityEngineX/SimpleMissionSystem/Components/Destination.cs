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
        OnDestinationReached?.Invoke();
        ConsoleProDebug.LogToFilter($"Mission {thisMission.name} Destination Reached : Type => {thisMission.missionType}","Missions");
    }
}