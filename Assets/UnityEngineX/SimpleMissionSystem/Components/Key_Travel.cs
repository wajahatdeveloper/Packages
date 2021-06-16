using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Key_Travel : MonoBehaviour
{
    public string missionName;
    [Tag] public string tagToCheck = "";
    public UnityEvent OnKeyApproached;

    private SimpleMission _thisMission;

    private void Start()
    {
        _thisMission = SimpleMission.missionList[missionName];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(tagToCheck)) { return; }
        OnKeyApproached?.Invoke();
        ConsoleProDebug.LogToFilter($"Mission {_thisMission.name} Key Approached : Type => {_thisMission.missionType}","Missions");
    }
}