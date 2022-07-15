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
        this.transform.localScale *= ((range * 0.25f)+1);
        _rb = GetComponent<Rigidbody2D>();
        _rb.transform.position = PlayerController.Instance.GetPlayerPosition();
        Invoke("SelfDestory", skillTime);
        

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