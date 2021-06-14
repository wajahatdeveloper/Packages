using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using MyBox;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[System.Serializable]
public class SpawnItem
{
    public string nameIdentifier;
    public GameObject prefabToSpawn;
    [Range(0,100)] public int chanceToSpawn;
    public int itemCount = 1;
    public float spawnDelay;
    public float spawnDelayRandom;
    public bool spawnOnRoot = false;
    public bool autoSpawn = true;
}

public class SpawnPoint : MonoBehaviour
{
    public List<SpawnItem> items;
    public int stopAfterCount = -1;
    public float startDelay;
    public bool logToFilter = true;
    public bool useEvents = false;
    [ConditionalField(nameof(useEvents))] public UnityEvent<SpawnItem> OnSpawnedItem;

    private int _count = 0;
    
    private void OnEnable()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(startDelay);

        while (stopAfterCount == -1 || stopAfterCount > 0)
        {
            foreach (SpawnItem item in items)
            {
                if (_count >= stopAfterCount) { yield break; }
                if (item.autoSpawn == false) { continue; }
                if (item.itemCount <= 0) { continue; }
                if (Random.Range(0,100) <= item.chanceToSpawn)
                {
                    this.Invoke(()=>
                    {
                        SpawnItem(item);
                    },item.spawnDelay + Random.Range(0f,item.spawnDelayRandom));
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private GameObject SpawnItem(SpawnItem item)
    {
        var spawnedItem = LeanPool.Spawn(item.prefabToSpawn, transform.position, transform.rotation, (item.spawnOnRoot)?null:transform);
        if (logToFilter)
        {
            ConsoleProDebug.LogToFilter($"{item.nameIdentifier} spawned by {name} at {spawnedItem.transform.position}","Create");
        }
        item.itemCount--;
        _count++;
        if (useEvents) { OnSpawnedItem?.Invoke(item); }
        return spawnedItem;
    }

    public void SpawnItem(string itemIdentifier)
    {
        SpawnItem(items.Single(x => x.nameIdentifier == itemIdentifier));
    }
}