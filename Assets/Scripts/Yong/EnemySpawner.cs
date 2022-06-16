using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _enemyPool = new Dictionary<string, Queue<GameObject>>();
    
    private Dictionary<string, int> _enemyList = new Dictionary<string, int>();

    public string[] enemyName;

    public int[] enemyNum;
    
    private GameObject _player;
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
                    string assertPath = "Assets/Prefab/Enemy/" + enemyName[i] + ".prefab";
                    GameObject enemy = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath(assertPath, typeof(GameObject)));
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
    public void ClearPool()
    {
        _enemyList.Clear();
        _enemyPool.Clear();
    }
}
