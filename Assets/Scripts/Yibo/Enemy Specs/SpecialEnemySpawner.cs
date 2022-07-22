using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpecialEnemySpawner : MonoBehaviour
{
    public GameObject[] firstLevelEliteEnemy;
    public GameObject firstLevelBoss;

    public int eliteEnemyCd = 60;


    private int enemySpawnMarker = 0;
    private float secondMarker = 0f;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (secondMarker > eliteEnemyCd && enemySpawnMarker < firstLevelEliteEnemy.Length)
        {
            Debug.Log("Spawn " + enemySpawnMarker + "elite");
            secondMarker = 0f;
            SpecialEnemySpawn(firstLevelEliteEnemy[enemySpawnMarker]);
            enemySpawnMarker++;
        }

        secondMarker += Time.deltaTime;
    }

    void SpecialEnemySpawn(GameObject spawnTarget)
    {
        Vector3 tempVector3 = EnemySpawner.GetRandomPosition();
        GameObject newSpecialEnemy = Instantiate(spawnTarget, tempVector3, Quaternion.identity);
    }
}
