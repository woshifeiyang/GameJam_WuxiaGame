using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    public float enemySpawnCd;

    public string currentPool;

    public const float screenWidthUnit = 8.5f;
    public const float screenHeightUnit = 19f;

    public EnemyObjectPool objectPoolHolder;

    public int enemySpawningCount = 0;
    public int monsterOnField = 0;
    public float timer = 0f;

    protected override void InitAwake()
    {
        base.InitAwake();
        objectPoolHolder = GetComponent<EnemyObjectPool>();
    }

    private void FixedUpdate()
    {
        updateSpawnCd();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            Debug.Log("Spawned enemy per sec:" + enemySpawningCount);
            Debug.Log("Current Enemy Count :" + monsterOnField);
            timer = 0f;
            enemySpawningCount = 0;
        }
    }

    void updateSpawnCd()
    {
        float spawnCdResult = 1f;

        int poolCurrentCount = objectPoolHolder.numCountOfPool[currentPool];
        int poolMaxCount = objectPoolHolder.numCountOfPool[currentPool + "Max"];
        monsterOnField = objectPoolHolder.numCountOfPool[currentPool + "OnField"];

        spawnCdResult = (float)monsterOnField / (float)poolMaxCount;

        enemySpawnCd = spawnCdResult;

        
    }

    public void SpawnEnemy()
    {
        StartCoroutine(SpawnEnemy_C());
    }
    
    IEnumerator SpawnEnemy_C()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnCd);
            GameObject enemy = EnemyObjectPool.EnemyObjectPoolInstance.GetObjectFromPool("First"); //  后期要改
            if (enemy)
            {
                enemy.transform.position = GetRandomPosition();
                enemy.transform.SetParent(EnemyObjectPool.EnemyObjectPoolInstance.objectOutOfPool.transform);
                enemy.SetActive(true);

                enemySpawningCount++;
            }
            else
            {
                Debug.Log("enemy is null, please check whether the name of pool is right or pool is null");
            }
        }
    }
    
    public static Vector2 GetRandomPosition()
    {
        float playerPositionX = PlayerController.Instance.GetPlayerPosition().x;
        float playerPositionY = PlayerController.Instance.GetPlayerPosition().y;

        float direction = Random.Range(0, 100);
        // 生成在玩家左右两侧
        if (direction <= 30)
        {
            float x;
            float y = Random.Range(playerPositionY - (screenHeightUnit / 2), playerPositionY + (screenHeightUnit / 2));
            int left = Random.Range(0, 100);
            float randomNum = Random.Range(0.5f, 1.5f);
            if (left <= 50)
            {
                x = playerPositionX - (screenWidthUnit / 2) - 0.3f;
                //x = Random.Range(playerPositionX - (screenWidthUnit/2), playerPositionX - (screenWidthUnit/2) - randomNum);
            }
            else
            {
                x = playerPositionX + (screenWidthUnit / 2) + 0.3f;
                //x = Random.Range(playerPositionX + (screenWidthUnit/2), playerPositionX + (screenWidthUnit/2) + randomNum);
            }

            return new Vector2(x, y);
        }
        // 生成在玩家上下两侧
        else
        {
            float randomNum = Random.Range(0.0f, screenWidthUnit);
            float x = Random.Range(playerPositionX + randomNum, playerPositionX - randomNum);
            float y;
            int top = Random.Range(0, 100);
            if (top <= 65)
            {
                y = playerPositionY + (screenHeightUnit / 2) + 1f;
                //y = Random.Range(playerPositionY + 6.0f, playerPositionY + 6.0f + randomNum);
            }
            else
            {
                y = playerPositionY - (screenHeightUnit / 2) + 1f;
                //y = Random.Range(playerPositionY - 6.0f, playerPositionY - 6.0f - randomNum);
            }
            return new Vector2(x, y);
        }
        
    }
}
