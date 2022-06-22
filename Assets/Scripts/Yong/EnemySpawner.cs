using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float enemySpawnCd;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1.0f, enemySpawnCd);        //后期要改
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        GameObject enemy = EnemyObjectPool.EnemyObjectPoolInstance.GetObjectFromPool("First");      //  后期要改
        if (enemy)
        {
            enemy.transform.position = GetRandomPosition();
            enemy.transform.SetParent(EnemyObjectPool.EnemyObjectPoolInstance.objectOutOfPool.transform);
            enemy.SetActive(true);
            enemy.GetComponent<Monster>().isDead = false;
            enemy.GetComponent<Monster>().SetMoveSpeed();
        }
        else
        {
            Debug.Log("enemy is null, please check whether the name of pool is right or pool is null");
        }
    }
    
    Vector2 GetRandomPosition()
    {
        float playerPositionX = PlayerController.PlayerControllerInstance.GetPlayerPosition().x;
        float playerPositionY = PlayerController.PlayerControllerInstance.GetPlayerPosition().y;
        //float playerPositionX = PlayerJoyController.PlayerControllerInstance.GetPlayerPosition().x;
        //float playerPositionY = PlayerJoyController.PlayerControllerInstance.GetPlayerPosition().y;


        float direction = Random.Range(0, 100);
        // 生成在玩家左右两侧
        if (direction <= 25)
        {
            float x;
            float y = Random.Range(playerPositionY - 6.0f, playerPositionY + 6.0f);
            int left = Random.Range(0, 1);
            float randomNum = Random.Range(1.0f, 3.0f);
            if (left == 1)
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
            int top = Random.Range(0, 1);
            if (top == 1)
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
