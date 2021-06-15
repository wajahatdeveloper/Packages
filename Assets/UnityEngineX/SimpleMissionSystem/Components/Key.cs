using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    public string identifier;
    [Tag] public string tagToCheck = "";
    public UnityEvent OnKeyApproached;

    [HideInInspector] public SimpleMission thisMission;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(tagToCheck)) { return; }
        OnKeyApproached?.Invoke();
        ConsoleProDebug.LogToFilter($"Mission {thisMission.name} Key Approached : Type => {thisMission.missionType}","Missions");
    }
}