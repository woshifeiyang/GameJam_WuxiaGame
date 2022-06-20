using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameObject _player;

    private GameObject _objectInPool;

    private GameObject _objectOutOfPool;

    public float enemySpawnCd;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _objectInPool = new GameObject();
        _objectInPool.name = "objectInPool";
        _objectOutOfPool = new GameObject();
        _objectOutOfPool.name = "objectOutOfPool";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    GameObject GetObjectFromPool(string poolName)
    {
        /*if (_enemyPool[poolName].Count != 0)
        {
            GameObject go = _enemyPool[poolName].Dequeue();
            go.SetActive(true);
            go.transform.SetParent(_objectOutOfPool.transform);
            return go;
        }
        Debug.Log("Pool is nullptr");*/
        return null;
    }
    
    Vector2 GetRandomPosition()
    {
        float playerPositionX = _player.transform.position.x;
        float playerPositionY = _player.transform.position.y;

        int direction = Random.Range(0, 1);
        // 生成在玩家左右两侧
        if (direction == 0)
        {
            float randomNum = Random.Range(1.0f, 3.0f);
            float x = Random.Range(playerPositionX - 2.5f + randomNum, playerPositionX + 2.5f + randomNum);
            float y = Random.Range(playerPositionY - randomNum, playerPositionY + randomNum);
            return new Vector2(x, y);
        }
        // 生成在玩家上下两侧
        else
        {
            float randomNum = Random.Range(1.0f, 3.0f);
            float x = Random.Range(playerPositionX + randomNum, playerPositionX + randomNum);
            float y = Random.Range(playerPositionY - 6.0f + randomNum, playerPositionY + 6.0f + randomNum);
            return new Vector2(x, y);
        }
        
    }
}
