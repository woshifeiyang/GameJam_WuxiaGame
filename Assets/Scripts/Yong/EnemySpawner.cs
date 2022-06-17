using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _enemyPool = new Dictionary<string, Queue<GameObject>>();
    
    private Dictionary<string, int> _enemyList = new Dictionary<string, int>();

    public string[] enemyName;

    public int[] enemyNum;
    
    private GameObject _player;
    
    [Serializable]
    public struct EnemyStruct
    {
        public string name;
        public int count;
    }

    public EnemyStruct[] firstPool;
    public EnemyStruct[] secondPool;
    public EnemyStruct[] thirdPool;
    public EnemyStruct[] fourthPool;
    public EnemyStruct[] fifthPool;
    public EnemyStruct[] sixthPool;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
        ClearPool();
        InitEnemyList();
        InitEnemyPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 通过玩家设置的怪物数量初始化怪物列表
    /// </summary>
    void InitEnemyList()
    {
        if (enemyName.Length != enemyNum.Length)
        {
            Debug.Log("The number of EnemyNum array doesn't match EnemyName array");
        }
        else
        {
            for (int i = 0; i < enemyName.Length; i++)
            {
                _enemyList.Add(enemyName[i], enemyNum[i]);
            }
        }
    }
    /// <summary>
    /// 通过怪物的名字和数量填充对应的对象池
    /// </summary>
    void InitEnemyPool()
    {
        if (_enemyList.Count == 0)
        {
            Debug.Log("EnemyList is null, please check");
        }
        else
        {
            for (int i = 0; i < enemyName.Length; i++)
            {
                _enemyPool.Add(enemyName[i], new Queue<GameObject>());
                for (int j = 0; j < enemyNum[i]; j++)
                {
                    string assertPath = "Prefab/Enemy/First/" + enemyName[i];
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool[enemyName[i]].Enqueue(enemy);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                    
                }
                Debug.Log(enemyName[i] + " has: " + _enemyPool[enemyName[i]].Count);
            }
            
        }
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
            float x = Random.Range(playerPositionX - 10.0f + randomNum, playerPositionX + 10.0f + randomNum);
            float y = Random.Range(playerPositionY - randomNum, playerPositionY + randomNum);
            return new Vector2(x, y);
        }
        // 生成在玩家上下两侧
        else
        {
            float randomNum = Random.Range(1.0f, 10.0f);
            float x = Random.Range(playerPositionX + randomNum, playerPositionX + randomNum);
            float y = Random.Range(playerPositionY - 5.0f + randomNum, playerPositionY + 5.0f + randomNum);
            return new Vector2(x, y);
        }
        
    }
    public void ClearPool()
    {
        _enemyList.Clear();
        _enemyPool.Clear();
    }
}
