using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpecialEnemySpawner : MonoBehaviour
{
    [Serializable]
    public struct SpecialEnemy
    {
        public GameObject specialEnemy;
        public float spawnSecond;
    }
    public SpecialEnemy[] specialEnemyHolder;
    public float Timer = 0f;

    private int enemySpawnMarker = 0;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Timer > specialEnemyHolder[enemySpawnMarker].spawnSecond)
        {
            Debug.Log("Spawn " + enemySpawnMarker + "elite");
            SpecialEnemySpawn(specialEnemyHolder[enemySpawnMarker].specialEnemy);
            enemySpawnMarker++;
        }
        Timer += Time.deltaTime;
    }

    void SpecialEnemySpawn(GameObject spawnTarget)
    {
        Vector3 tempVector3 = EnemySpawner.GetRandomPosition();
        GameObject newSpecialEnemy = Instantiate(spawnTarget, tempVector3, Quaternion.identity);
    }
}
