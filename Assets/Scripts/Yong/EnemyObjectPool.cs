using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _enemyPool = new Dictionary<string, Queue<GameObject>>();

    public Dictionary<string, int> numCountOfPool = new Dictionary<string, int>();

    public GameObject objectInPool;

    public GameObject objectOutOfPool;
    
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
        objectInPool = new GameObject();
        objectInPool.name = "objectInPool";
        objectOutOfPool = new GameObject();
        objectOutOfPool.name = "objectOutOfPool";
        
        
        ClearPool();
        InitEnemyPool();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitEnemyPoolCount()
    {
        // current num for each pool
        numCountOfPool.Add("First", 0);
        numCountOfPool.Add("Second", 0);
        numCountOfPool.Add("Third", 0);
        numCountOfPool.Add("Fourth", 0);
        
        // constant max number of each pool
        numCountOfPool.Add("FirstMax", 0);
        numCountOfPool.Add("SecondMax", 0);
        numCountOfPool.Add("ThirdMax", 0);
        numCountOfPool.Add("FourthMax", 0);
        
        // current num of monsters on the field for each pool
        numCountOfPool.Add("FirstOnField", 0);
        numCountOfPool.Add("SecondOnField", 0);
        numCountOfPool.Add("ThirdOnField", 0);
        numCountOfPool.Add("FourthOnField", 0);
    }
    
    /// <summary>
    /// 将每个结构体数组中设定的怪物种类和数量填充进不同的对象池
    /// </summary>
    void InitEnemyPool()
    {
        //only first pool right now
        //init stats of pool first
        InitEnemyPoolCount();
        
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
                for (int j = 0; j < firstPool[i].count; ++j)
                {
                    GameObject enemy = (GameObject)Instantiate(Resources.Load(assertPath));
                    if (enemy)
                    {
                        enemy.SetActive(false);
                        _enemyPool["First"].Enqueue(enemy);
                        enemy.transform.SetParent(objectInPool.transform);
                    }
                    else
                    {
                        Debug.Log("Can not find enemy object, please check if the prefab enemy's name is right");
                    }
                }
                Debug.Log("the number of " + firstPool[i].name + " is " + firstPool[i].count);
                
                // add number count of each pool
                numCountOfPool["FirstMax"] += firstPool[i].count;
                numCountOfPool["First"] = numCountOfPool["FirstMax"];
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
                        enemy.transform.SetParent(objectInPool.transform);
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
                        enemy.transform.SetParent(objectInPool.transform);
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
                        enemy.transform.SetParent(objectInPool.transform);
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
            
            // minus pool num by 1 
            numCountOfPool[poolName] --;
            numCountOfPool[poolName + "OnField"]++;
            return go;
        }
        Debug.Log("Pool is nullptr");
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
        
        _enemyPool[tempPoolName].Enqueue(ob);
        
        // add one to the pool number counter
        numCountOfPool[tempPoolName]++;
        numCountOfPool[tempPoolName + "OnField"]--;
        
        ob.SetActive(false);
        ob.transform.SetParent(EnemyObjectPool.EnemyObjectPoolInstance.objectInPool.transform);
    }
    
    public void ClearPool()
    {
        _enemyPool.Clear();
    }
}
