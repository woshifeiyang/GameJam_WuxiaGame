
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill : BulletSkillBase
{
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    public override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _rb.AddForce((PlayerController.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position).normalized * speed, ForceMode2D.Force);
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
