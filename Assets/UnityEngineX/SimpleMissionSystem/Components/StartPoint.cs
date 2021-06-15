using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public bool repositionOnStart = false;
    [ConditionalField(nameof(repositionOnStart))] public Transform PlayerTransform;
    public bool spawnOnStart = false;
    [ConditionalField(nameof(spawnOnStart))] public GameObject playerPrefab;

    public void MissionStart()
    {
        if (repositionOnStart)
        {
            PlayerTransform.position = transform.position;
            PlayerTransform.rotation = transform.rotation;
            SimpleMission.CurrentPlayerInstance = PlayerTransform;
        }
        else if (spawnOnStart)
        {
            var player = Instantiate(playerPrefab, transform.position, transform.rotation);
            SimpleMission.CurrentPlayerInstance = player.transform;
        }
    }
}