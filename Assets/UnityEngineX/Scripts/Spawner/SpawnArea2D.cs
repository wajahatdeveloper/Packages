using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnItem2D
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

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnArea2D : MonoBehaviour 
{
    public List<SpawnItem2D> items;
    public int stopAfterCount = -1;
    public float startDelay;
    public bool logToFilter = true;
    public bool useEvents = false;
    [ConditionalField(nameof(useEvents))] public UnityEvent<SpawnItem2D> OnSpawnedItem;

    private int _count = 0;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(startDelay);

        while (stopAfterCount == -1 || stopAfterCount > 0)
        {
            foreach (SpawnItem2D item in items)
            {
                if (_count >= stopAfterCount && stopAfterCount != -1) { yield break; }
                if (item.autoSpawn == false) { continue; }
                if (item.itemCount <= 0) { continue; }
                if (Random.Range(0,100) <= item.chanceToSpawn)
                {
                    this.Invoke(()=>
                    {
                        SpawnItem2D(item);
                    },item.spawnDelay + Random.Range(0f,item.spawnDelayRandom));
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private GameObject SpawnItem2D(SpawnItem2D item)
    {
        var position = _boxCollider.GetRandomPointInsideCollider();
        var spawnedItem = LeanPool.Spawn(item.prefabToSpawn, position, transform.rotation, (item.spawnOnRoot)?null:transform);
        if (logToFilter)
        {
            ConsoleProDebug.LogToFilter($"{item.nameIdentifier} spawned by {name} at {spawnedItem.transform.position}","Create");
        }
        item.itemCount--;
        _count++;
        if (useEvents) { OnSpawnedItem?.Invoke(item); }
        return spawnedItem;
    }

    public void SpawnItem2D(string itemIdentifier)
    {
        SpawnItem2D(items.Single(x => x.nameIdentifier == itemIdentifier));
    }
}