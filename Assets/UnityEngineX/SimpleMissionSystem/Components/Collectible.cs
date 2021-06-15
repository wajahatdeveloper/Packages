using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour 
{
    public string identifier;
    [Tag] public string tagToCheck = "";
    public UnityEvent OnCollected;

    [HideInInspector] public SimpleMission thisMission;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(tagToCheck)) { return; }
        OnCollected?.Invoke();
        ConsoleProDebug.LogToFilter($"Mission {thisMission.name} Collectible Collected : Type => {thisMission.missionType}","Missions");
    }
}