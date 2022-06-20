using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _enemyPool = new Dictionary<string, Queue<GameObject>>();
    
    private GameObject _player;

    private GameObject _objectInPool;

    private GameObject _objectOutOfPool;
    
    public static EnemyObjectPool EnemyObjectPoolInstance;      // 子弹池单例
    
    [Serializable]
    public struct EnemyStruct
    {
        public string name;
        public int count;
    }

    public EnemyObjectPool.EnemyStruct[] firstPool;
    public EnemyObjectPool.EnemyStruct[] secondPool;
    public EnemyObjectPool.EnemyStruct[] thirdPool;
    public EnemyObjectPool.EnemyStruct[] fourthPool;
    
    private void Awake()
    {
        EnemyObjectPoolInstance = this;
    }
    
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

        if (firstPool.Length != 0)
        {
            Debug.Log("----This is the first pool----");
            for (int i = 0; i < firstPool.Length; ++i)
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
            for (int i = 0; i < secondPool.Length; ++i)
            {
                string assertPath = "Prefab/Enemy/Second/" + secondPool[i].name;
                for (int j = 0; j < secondPool[i].count; j++)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["Second"].Enqueue(enemy);
                        enemy.transform.SetParent(_objectInPool.transform);
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
                        enemy.transform.SetParent(_objectInPool.transform);
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
                        enemy.transform.SetParent(_objectInPool.transform);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + fourthPool[i].name + " is " + fourthPool[i].count);
            }
        }
        
    }

    public GameObject GetObjectFromPool(string poolName)
    {
        if (_enemyPool[poolName].Count != 0)
        {
            GameObject go = _enemyPool[poolName].Dequeue();
            return go;
        }
        Debug.Log("Pool is nullptr");
        return null;
    }
    
    public void ClearPool()
    {
        _enemyPool.Clear();
    }
}
