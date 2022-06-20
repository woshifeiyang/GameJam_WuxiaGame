using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameObject _objectInPool;

    private GameObject _objectOutOfPool;

    public float enemySpawnCd;

    // Start is called before the first frame update
    void Start()
    {
        _objectInPool = new GameObject();
        _objectInPool.name = "objectInPool";
        _objectOutOfPool = new GameObject();
        _objectOutOfPool.name = "objectOutOfPool";
        
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
            enemy.transform.SetParent(_objectOutOfPool.transform);
            enemy.SetActive(true);
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

        int direction = Random.Range(0, 1);
        // 生成在玩家左右两侧
        if (direction == 0)
        {
            float randomNum = Random.Range(1.0f, 3.0f);
            float x = Random.Range(playerPositionX - 3.0f - randomNum, playerPositionX + 3.0f + randomNum);
            float y = Random.Range(playerPositionY - randomNum, playerPositionY + randomNum);
            return new Vector2(x, y);
        }
        // 生成在玩家上下两侧
        else
        {
            float randomNum = Random.Range(1.0f, 3.0f);
            float x = Random.Range(playerPositionX + randomNum, playerPositionX + randomNum);
            float y = Random.Range(playerPositionY - 6.0f - randomNum, playerPositionY + 6.0f + randomNum);
            return new Vector2(x, y);
        }
        
    }
}
