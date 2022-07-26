using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDetector : MonoSingleton<EnemyDetector>
{
    private CircleCollider2D _cc;

    private bool _hasFoundEnemy;
    
    private GameObject _nearestEnemy;

    public List<GameObject> enemyList;

    protected override void InitAwake()
    {
        base.InitAwake();
        _cc = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        enemyList = new List<GameObject>();
        _hasFoundEnemy = false;
        StartCoroutine("FindNearestTarget");
    }

    // Update is called once per frame
    void Update()
    {
        if (_nearestEnemy)
        {
            Debug.DrawLine(transform.position, _nearestEnemy.transform.position, Color.red);
        }
    }
    public IEnumerator FindNearestTarget()
    {
        float radius = _cc.radius;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_hasFoundEnemy || _cc.radius >= 10.0f)
            {
                _cc.radius = radius;
                _hasFoundEnemy = false;
            }
            else _cc.radius += 0.5f;
        }
    }
    // 敌人探测器
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy") || other.gameObject.CompareTag("Boss")) && other.gameObject.GetComponent<Monster>().isDead == false)
        {
            _nearestEnemy = other.gameObject;
            _hasFoundEnemy = true;
        }
    }

    public Vector3 GetNearestEnemyLoc()
    {
        if (_nearestEnemy != null)
        {
            return _nearestEnemy.transform.position;
        }
        return new Vector3(1, 0, 0);
    }

    public static List<T> GetRandomElements<T>(IEnumerable<T> list, int elementsCount)
    {
        return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
    }

    public static List<T> GetRandomElement<T>(IEnumerable<T> list)
    {
        return list.OrderBy(arg => Guid.NewGuid()).Take(1).ToList();
    }
    
}
