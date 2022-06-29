using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Skill401 : ScopeSkillBase
{
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    public override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke("SelfDestory", skillTime);
        //_rb.AddForce((PlayerController.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position).normalized * speed, ForceMode2D.Force);
        //_rb.AddForce((PlayerController.Instance.transform.position - PlayerController.Instance.GetNearestEnemyLoc()).normalized * speed, ForceMode2D.Force);

        // 持续时间结束时销毁自身
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
        
    }
}