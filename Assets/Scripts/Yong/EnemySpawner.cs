using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float enemySpawnCd;

    public string currentPool;

    public float screenWidthUnit = 5.8f;
    public float screenHeightUnit = 12.5f;

    public EnemyObjectPool objectPoolHolder;
    // Start is called before the first frame update
    void Start()
    {
        objectPoolHolder = GetComponent<EnemyObjectPool>();
        
        //InvokeRepeating(nameof(SpawnEnemy), 1.0f, enemySpawnCd); //后期要改
        InvokeRepeating(nameof(updateSpawnCd), 1.0f, 0.1f);
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void updateSpawnCd()
    {
        float spawnCdResult = 1f;

        int poolCurrentCount = objectPoolHolder.numCountOfPool[currentPool];
        int poolMaxCount = objectPoolHolder.numCountOfPool[currentPool + "Max"];
        int monsterOnField = objectPoolHolder.numCountOfPool[currentPool + "OnField"];

        spawnCdResult = (float)monsterOnField / (float)poolMaxCount;
        
        enemySpawnCd = spawnCdResult;
    }

    IEnumerator SpawnEnemy()
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
            }
            else
            {
                Debug.Log("enemy is null, please check whether the name of pool is right or pool is null");
            }
        }
    }
    
    Vector2 GetRandomPosition()
    {
        float playerPositionX = PlayerController.Instance.GetPlayerPosition().x;
        float playerPositionY = PlayerController.Instance.GetPlayerPosition().y;

        float direction = Random.Range(0, 100);
        // 生成在玩家左右两侧
        if (direction <= 1)
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
                y = playerPositionY - (screenHeightUnit / 2);
                //y = Random.Range(playerPositionY - 6.0f, playerPositionY - 6.0f - randomNum);
            }
            return new Vector2(x, y);
        }
        
    }
}
