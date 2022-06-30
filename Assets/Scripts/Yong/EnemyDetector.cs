using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoSingleton<EnemyDetector>
{
    private CircleCollider2D _cc;
    
    private bool _hasFoundEnemy;
    
    private GameObject _nearestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CircleCollider2D>();

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
    IEnumerator FindNearestTarget()
    {
        float radius = _cc.radius;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
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
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Monster>().isDead == false)
        {
            _nearestEnemy = other.gameObject;
            _hasFoundEnemy = true;
        }
    }

    public Vector3 GetNearestEnemyLoc()
    {
        return _nearestEnemy.transform.position;
    }
}
