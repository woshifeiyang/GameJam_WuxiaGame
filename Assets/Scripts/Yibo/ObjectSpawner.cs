using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int maxAmount = -1;
    [SerializeField, Range(0f, 10f)] private float secondsBetweenSpawns = 1f;
    [SerializeField] private bool stopWhenFull;
    [SerializeField] private bool startOnAwake = true;
    [SerializeField, Range(0f, 5f)] private float overlapRadius = 0.5f;
    [SerializeField] private LayerMask overlapMask = 0;

    private new BoxCollider2D collider;
    private Bounds colliderBounds;

    private float nextSpawnTime;
    private List<GameObject> spawnedObjects;
    private bool hasCollider;

    private bool isRunning;

    private void Awake()
    {
        hasCollider = TryGetComponent(out collider);
        spawnedObjects = new List<GameObject>();
        
        isRunning = startOnAwake;
    }

    public void StartSpawning()
    {
        isRunning = true;
    }

    public bool TrySpawning()
    {
        Vector3 spawnPoint = hasCollider ? GetRandomPointInsideCollider(collider) : transform.position;

        Collider2D hit = Physics2D.OverlapCircle((Vector2)spawnPoint, overlapRadius, overlapMask);
        if (hit && !hit.isTrigger)
            return false;
        
        spawnedObjects.Add(Instantiate(objectToSpawn, spawnPoint, Quaternion.identity));
        return true;
    }

    private void Update()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null)
                spawnedObjects.RemoveAt(i);
        }
        
        if(!isRunning)
            return;
        
        if(maxAmount > -1 && spawnedObjects.Count >= maxAmount)
        {
            if (stopWhenFull)
                isRunning = false;
            return;
        }
        
        if(Time.time < nextSpawnTime)
            return;

        if (!hasCollider)
        {
            TrySpawning();
        }
        else
        {
            int attempts = 0;
            int maxAttempts = 5;
            while (!TrySpawning() && attempts < maxAttempts)
            {
                attempts++;
            }
        }
        
        nextSpawnTime = Time.time + secondsBetweenSpawns;
    }
    private Vector3 GetRandomPointInsideCollider( BoxCollider2D boxCollider )
    {
        Vector2 extents = boxCollider.size / 2f;
        Vector2 point = new Vector2(
            Random.Range( -extents.x, extents.x ),
            Random.Range( -extents.y, extents.y )
        )  + boxCollider.offset;
        return boxCollider.transform.TransformPoint( point );
    }
}