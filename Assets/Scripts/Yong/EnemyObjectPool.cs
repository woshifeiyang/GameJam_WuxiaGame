using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _enemyPools = new Dictionary<string, Queue<GameObject>>();

    public Dictionary<string, int> EnemyNumInPool = new Dictionary<string, int>();

    public GameObject objectInPool;

    public GameObject objectOutOfPool;
    
    public static EnemyObjectPool Instance;      
    
    [Serializable]
    public struct EnemyStruct
    {
        public string name;
        public int count;
    }

    public EnemyStruct[] enemyArray;
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyArray.Length; i++)
        {
            enemyArray[i].count = 500;
        }

        objectInPool = new GameObject();
        objectInPool.name = "objectInPool";
        objectOutOfPool = new GameObject();
        objectOutOfPool.name = "objectOutOfPool";

        ClearPool();
        InitEnemyPool();
    }

    void InitEnemyPoolCount(EnemyStruct enemy)
    {
        // 场外该种类敌人数量
        EnemyNumInPool.Add(enemy.name, 0);
        
        // 该种类敌人池子的大小
        EnemyNumInPool.Add(enemy.name + "Max", enemy.count);

        // 场内该种类敌人数量
        EnemyNumInPool.Add(enemy.name + "OnField", 0);
    }

    public int countEnemyTotalNum()
    {
        int result = 0;

        foreach(EnemyStruct entry in enemyArray)
        {
            String temp = entry.name + "OnField";
            result += EnemyNumInPool[temp];
        }
        //Debug.Log("result" + result);
        return result;
    }
    /// <summary>
    /// 将每个结构体数组中设定的怪物种类和数量填充进不同的对象池
    /// </summary>
    void InitEnemyPool()
    {
        // 每个怪物创建自己的对象池
        for (int i = 0; i < enemyArray.Length; ++i)
        {
            _enemyPools.Add(enemyArray[i].name, new Queue<GameObject>());
            
            InitEnemyPoolCount(enemyArray[i]);
            // 实例化对象池内的对象
            Debug.Log("---Initializing the " + enemyArray[i].name + " pool---");
            if (enemyArray[i].count != 0)
            {
                for (int j = 0; j < enemyArray[i].count; ++j)
                {
                    string assertPath = "Prefab/Enemy/NormalEnemy/" + enemyArray[i].name;
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPools[enemyArray[i].name].Enqueue(enemy);
                        enemy.transform.SetParent(objectInPool.transform);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
            }
        }
    }

    public GameObject GetObjectFromPool(string poolName)
    {
        if (_enemyPools[poolName].Count != 0)
        {
            GameObject go = _enemyPools[poolName].Dequeue();
            // Update the number of enemies inside and outside the pool
            EnemyNumInPool[poolName]--;
            EnemyNumInPool[poolName + "OnField"]++;
            return go;
        }
        return null;
    }

    public void PutObjectInPool(GameObject ob)
    {
        if (ob == null)
        {
            Debug.Log("Can not put null object in the pool");
            return;
        }

        string tempPoolName = ob.GetComponent<Monster>().poolBelongTo;
        _enemyPools[tempPoolName].Enqueue(ob);
        
        // add one to the pool number counter
        EnemyNumInPool[tempPoolName]++;
        EnemyNumInPool[tempPoolName + "OnField"]--;
        
        ob.SetActive(false);
        ob.transform.SetParent(Instance.objectInPool.transform);
    }
    
    public void ClearPool()
    {
        _enemyPools.Clear();
    }
}
