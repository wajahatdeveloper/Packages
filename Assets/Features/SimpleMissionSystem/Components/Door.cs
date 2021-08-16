using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour 
{
    public string identifier;
    [Tag] public string tagToCheck = "";
    public UnityEvent OnDoorApproached;

    [HideInInspector] public SimpleMission thisMission;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(tagToCheck)) { return; }
        OnDoorApproached?.Invoke();
        ConsoleProDebug.LogToFilter($"Mission {thisMission.name} Door Approached : Type => {thisMission.missionType}","Missions");
    }
}