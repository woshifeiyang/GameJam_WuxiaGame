
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill101 : BulletSkillBase
{
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    public override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector3 v = EnemyDetector.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position;
        v.z = 0;
        float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        _rb.AddForce(v.normalized * speed, ForceMode2D.Force);
        // 持续时间结束时销毁自身
        Invoke("SelfDestory", skillTime);
    }
    // Update is called once per frame
    public override void Update()
    {
        
    }

    private void SelfDestory()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy"))
        {
            --skillPene;
            if (skillPene == 0)
            {
                SelfDestory();
            }
        }
    }
}
