using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _enemyPool = new Dictionary<string, Queue<GameObject>>();

    private GameObject _player;

    private GameObject _objectInPool;

    private GameObject _objectOutOfPool;

    public float enemySpawnCd;
    
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
        _objectInPool = new GameObject();
        _objectInPool.name = "objectInPool";
        _objectOutOfPool = new GameObject();
        _objectOutOfPool.name = "objectOutOfPool";

        ClearPool();
        InitEnemyPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// 将每个结构体数组中设定的怪物种类和数量填充进不同的对象池
    /// </summary>
    void InitEnemyPool()
    {
        _enemyPool.Add("First", new Queue<GameObject>());
        _enemyPool.Add("Second", new Queue<GameObject>());
        _enemyPool.Add("Third", new Queue<GameObject>());
        _enemyPool.Add("Fourth", new Queue<GameObject>());
        _enemyPool.Add("Fifth", new Queue<GameObject>());
        _enemyPool.Add("Sixth", new Queue<GameObject>());

        if (firstPool.Length != 0)
        {
            Debug.Log("----This is the first pool----");
            for (int i = 0; i < firstPool.Length; i++)
            {
                string assertPath = "Prefab/Enemy/First/" + firstPool[i].name;
                for (int j = 0; j < firstPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["First"].Enqueue(enemy);
                        enemy.transform.SetParent(_objectInPool.transform);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + firstPool[i].name + " is " + firstPool[i].count);
            }
        }
        if (secondPool.Length != 0)
        {
            Debug.Log("----This is the second pool----");
            for (int i = 0; i < secondPool.Length; i++)
            {
                string assertPath = "Prefab/Enemy/Second/" + secondPool[i].name;
                for (int j = 0; j < secondPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["Second"].Enqueue(enemy);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + secondPool[i].name + " is " + secondPool[i].count);
            }
        }
        if (thirdPool.Length != 0)
        {
            Debug.Log("----This is the third pool----");
            for (int i = 0; i < thirdPool.Length; i++)
            {
                string assertPath = "Prefab/Enemy/Third/" + thirdPool[i].name;
                for (int j = 0; j < thirdPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["Third"].Enqueue(enemy);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + thirdPool[i].name + " is " + thirdPool[i].count);
            }
        }
        if (fourthPool.Length != 0)
        {
            Debug.Log("----This is the fourth pool----");
            for (int i = 0; i < fourthPool.Length; i++)
            {
                string assertPath = "Prefab/Enemy/Fourth/" + fourthPool[i].name;
                for (int j = 0; j < fourthPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["Fourth"].Enqueue(enemy);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + fourthPool[i].name + " is " + fourthPool[i].count);
            }
        }
        if (fifthPool.Length != 0)
        {
            Debug.Log("----This is the fifth pool----");
            for (int i = 0; i < fifthPool.Length; i++)
            {
                string assertPath = "Prefab/Enemy/Fifth/" + fifthPool[i].name;
                for (int j = 0; j < fifthPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["Fifth"].Enqueue(enemy);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + fifthPool[i].name + " is " + fifthPool[i].count);
            }
        }
        if (sixthPool.Length != 0)
        {
            Debug.Log("----This is the sixth pool----");
            for (int i = 0; i < sixthPool.Length; i++)
            {
                string assertPath = "Prefab/Enemy/Sixth/" + sixthPool[i].name;
                for (int j = 0; j < sixthPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["Sixth"].Enqueue(enemy);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + sixthPool[i].name + " is " + sixthPool[i].count);
            }
        }
    }

    GameObject GetObjectFromPool(string poolName)
    {
        if (_enemyPool[poolName].Count != 0)
        {
            GameObject go = _enemyPool[poolName].Dequeue();
            go.SetActive(true);
            go.transform.SetParent(_objectOutOfPool.transform);
            return go;
        }
        Debug.Log("Pool is nullptr");
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
        _enemyPool.Clear();
    }
}
