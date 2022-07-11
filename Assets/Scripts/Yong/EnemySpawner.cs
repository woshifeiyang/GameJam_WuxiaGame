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

    public EnemyObjectPool objectPoolHolder;
    // Start is called before the first frame update
    void Start()
    {
        objectPoolHolder = GetComponent<EnemyObjectPool>();
        
        InvokeRepeating(nameof(SpawnEnemy), 1.0f, enemySpawnCd); //后期要改
        InvokeRepeating(nameof(updateSpawnCd), 1.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateSpawnCd()
    {
        float spawnCdResult = 1f;
        
        
        
        enemySpawnCd = spawnCdResult;
    }

    void SpawnEnemy()
    {
        GameObject enemy = EnemyObjectPool.EnemyObjectPoolInstance.GetObjectFromPool("First");      //  后期要改
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
    
    Vector2 GetRandomPosition()
    {
        float playerPositionX = PlayerController.Instance.GetPlayerPosition().x;
        float playerPositionY = PlayerController.Instance.GetPlayerPosition().y;

        float direction = Random.Range(0, 100);
        // 生成在玩家左右两侧
        if (direction <= 25)
        {
            float x;
            float y = Random.Range(playerPositionY - 6.0f, playerPositionY + 6.0f);
            int left = Random.Range(0, 100);
            float randomNum = Random.Range(1.0f, 3.0f);
            if (left <= 50)
            {
                x = Random.Range(playerPositionX - 3.0f, playerPositionX - 3.0f - randomNum);
            }
            else
            {
                x = Random.Range(playerPositionX + 3.0f, playerPositionX + 3.0f + randomNum);
            }

            return new Vector2(x, y);
        }
        // 生成在玩家上下两侧
        else
        {
            float randomNum = Random.Range(0.0f, 3.0f);
            float x = Random.Range(playerPositionX + randomNum, playerPositionX - randomNum);
            float y;
            int top = Random.Range(0, 100);
            if (top <= 50)
            {
                y = Random.Range(playerPositionY + 6.0f, playerPositionY + 6.0f + randomNum);
            }
            else
            {
                y = Random.Range(playerPositionY - 6.0f, playerPositionY - 6.0f - randomNum);
            }
            return new Vector2(x, y);
        }
        
    }
}
