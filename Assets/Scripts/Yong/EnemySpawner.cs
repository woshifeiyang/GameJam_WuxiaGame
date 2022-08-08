using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    public float enemySpawnCdBase;
    public float enemySpawnCdFinal;
    public float spawnCdFactorAdjust = 0.1f;
    private string _currentPool;

    private const float ScreenWidthUnit = 8.5f;
    private const float ScreenHeightUnit = 19f;

    public int enemySpawningCount = 0;
    public int monsterOnField = 0;
    private float _timer = 0f;
    

    //test dic 
    private Dictionary<string, float> difficultyOfEnemyPool = new Dictionary<string, float>();
    private string targetPool;

    // [game stage number][enemy id]
    public float[][] enemySpawnSpeed;

    public int totalStageCount = 0;

    public float finalSpawnCd = 1f;
    public EnemyObjectPool objectPoolComp;

    //enemy data struct
    [Serializable]
    public struct EnemyDifficultyData
    {
        public string name;
        public float spawnSpeed;
    }
    [Serializable]
    public struct Stages
    {
        public float startOfStageTime;
        public EnemyDifficultyData[] EnemyData;
    }

    public Stages[] difficultyOfStages;
    public int currentStageNum = 0;
    private Dictionary<string, float> enemySpawnRatio = new Dictionary<string, float>();

    public Queue<string> spawnQueue;
    private float Timer = 0f;

    public void InitEnemySpawner()
    {
        objectPoolComp = gameObject.GetComponent<EnemyObjectPool>();

        enemySpawnSpeed = new float[totalStageCount][];

        spawnQueue = new Queue<string>();
        spawnQueue.Enqueue("101");

        targetPool = "101";
        _currentPool = "101";

        InitSpawnSpeed();
        StageSwitch();
    }

    private void PutDataIntoDic()
    {
        difficultyOfEnemyPool.Clear();

        foreach(EnemyDifficultyData entry in difficultyOfStages[currentStageNum].EnemyData)
        {
            difficultyOfEnemyPool.Add(entry.name, entry.spawnSpeed);
        }
    }

    private float GetTotalSpawnSpeedInSecond()
    {
        float totalSpawnSpeed = 0f;

        foreach(KeyValuePair<string,float> entry in difficultyOfEnemyPool)
        {
            totalSpawnSpeed += entry.Value;
        }

        return 1/totalSpawnSpeed;
    }

    private void InitSpawnSpeed()
    {
        for (int m = 0; m < enemySpawnSpeed.Length; m++)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    int tempId = 100 + i * 10 + j;
                    enemySpawnSpeed[m][tempId] = 0;
                }
            }
        }

        PutDataIntoDic();
    }
    
    private void FixedUpdate()
    {
        UpdateSpawnCd();
        
        _timer += Time.fixedDeltaTime;
        if (_timer > 1f)
        {
            _timer = 0f;
            enemySpawningCount = 0;
        }

        Timer += Time.deltaTime;

        for (int i = currentStageNum; i < difficultyOfStages.Length; i++)
        {
            if (Timer > difficultyOfStages[i].startOfStageTime && currentStageNum != i)
            {
                currentStageNum = i;
                spawnQueue.Clear();
                StageSwitch();
                break;
            }
        }
    }
    private void Update()
    {
        UpdateSpawnQueue();
    }

    void UpdateSpawnQueue()
    {
        if (spawnQueue.Count < 100)
        {
            float randomNum = Random.Range(0, 1f);
            string tempResult = "101";
            foreach(KeyValuePair<string,float> entry in enemySpawnRatio)
            {
                if(entry.Value > randomNum)
                {
                    tempResult = entry.Key;
                    break;
                }
            }
            spawnQueue.Enqueue(tempResult);
        }
    }

    void StageSwitch()
    {
        float tempCursor = 0f;
        enemySpawnRatio.Clear();

        PutDataIntoDic();

        foreach (KeyValuePair<string, float> entry in difficultyOfEnemyPool)
        {
            if (entry.Value != 0)
            {
                float tempRatio = entry.Value * GetTotalSpawnSpeedInSecond();
                tempCursor += tempRatio;
                enemySpawnRatio.Add(entry.Key, tempCursor);
                Debug.Log("add " + entry.Key + " ratio " + tempCursor);
            }
        }

    }
    
    void UpdateSpawnCd()
    {
        enemySpawnCdBase = GetTotalSpawnSpeedInSecond();
        enemySpawnCdFinal = enemySpawnCdBase;
        if ((1/GetTotalSpawnSpeedInSecond() * 8) > objectPoolComp.countEnemyTotalNum()) 
        {
            float enemyMapFillFactor = 1f;
            enemyMapFillFactor = (float)(objectPoolComp.countEnemyTotalNum() + 1)/ 500f;
            enemySpawnCdFinal = enemySpawnCdBase * enemyMapFillFactor;
        }
        //Debug.Log("enemy spawn cd = " + enemySpawnCdFinal);
    }

    public void SpawnEnemy()
    {
        StartCoroutine(SpawnEnemy_C());
    }
    
    IEnumerator SpawnEnemy_C()
    {
        float cd = enemySpawnCdFinal;
        while (true)
        {
            yield return new WaitForSeconds(cd);
            //GameObject enemy = EnemyObjectPool.Instance.GetObjectFromPool(_currentPool);
            if (spawnQueue.Count > 0)
            {
                GameObject enemy = EnemyObjectPool.Instance.GetObjectFromPool(spawnQueue.Dequeue());//  后期要改
                if (enemy)
                {
                    enemy.transform.position = GetRandomPosition();
                    enemy.transform.SetParent(EnemyObjectPool.Instance.objectOutOfPool.transform);
                    enemy.SetActive(true);

                    enemySpawningCount++;
                }
                else
                {
                    Debug.Log("enemy is null, please check whether the name of pool is right or pool is null");
                }
            }
            cd = enemySpawnCdFinal;
        }
    }

    IEnumerator updateTargetPool()
    {
        yield return null;
    }

    private void SetEnemySpawningSpeed(string id, float targetSpeed)
    {
        difficultyOfEnemyPool[id] = targetSpeed;
    }

    private void ClearEnemySpawningSpeed()
    {
        foreach(KeyValuePair<string,float> entry in difficultyOfEnemyPool)
        {
            difficultyOfEnemyPool[entry.Key] = 0f;
        }
    }
    
    
    public static Vector2 GetRandomPosition()
    {
        float playerPositionX = PlayerController.Instance.GetPlayerPosition().x;
        float playerPositionY = PlayerController.Instance.GetPlayerPosition().y;

        float direction = Random.Range(0, 100);
        // 生成在玩家左右两侧
        if (direction <= 30)
        {
            float x;
            float y = Random.Range(playerPositionY - (ScreenHeightUnit / 2), playerPositionY + (ScreenHeightUnit / 2));
            int left = Random.Range(0, 100);
            float randomNum = Random.Range(0.5f, 1.5f);
            if (left <= 50)
            {
                x = playerPositionX - (ScreenWidthUnit / 2) - 0.3f;
                //x = Random.Range(playerPositionX - (screenWidthUnit/2), playerPositionX - (screenWidthUnit/2) - randomNum);
            }
            else
            {
                x = playerPositionX + (ScreenWidthUnit / 2) + 0.3f;
                //x = Random.Range(playerPositionX + (screenWidthUnit/2), playerPositionX + (screenWidthUnit/2) + randomNum);
            }

            return new Vector2(x, y);
        }
        // 生成在玩家上下两侧
        else
        {
            float randomNum = Random.Range(0.0f, ScreenWidthUnit);
            float x = Random.Range(playerPositionX + randomNum, playerPositionX - randomNum);
            float y;
            int top = Random.Range(0, 100);
            if (top <= 65)
            {
                y = playerPositionY + (ScreenHeightUnit / 2) + 1f;
                //y = Random.Range(playerPositionY + 6.0f, playerPositionY + 6.0f + randomNum);
            }
            else
            {
                y = playerPositionY - (ScreenHeightUnit / 2) + 1f;
                //y = Random.Range(playerPositionY - 6.0f, playerPositionY - 6.0f - randomNum);
            }
            return new Vector2(x, y);
        }
        
    }
}
