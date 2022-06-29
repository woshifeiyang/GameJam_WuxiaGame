using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Skill402 : BulletSkillBase
{
    private Rigidbody2D _rb;
    public bool first = true;
    // Start is called before the first frame update
    public override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce((PlayerController.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position).normalized * speed, ForceMode2D.Force);
        // 持续时间结束时销毁自身
        //Invoke("SelfDestory", skillTime);
    }
    // Update is called once per frame
    public override void Update()
    {
        if(first == false)
        {
            if(Vector3.Distance(_rb.transform.position, PlayerController.Instance.GetPlayerPosition()) <= 1.0f)
            {
                SelfDestory();
            }
            Vector3 playerPosition = PlayerController.Instance.GetPlayerPosition();
            _rb.MovePosition(_rb.transform.position + (playerPosition - _rb.transform.position).normalized * speed * 0.1f * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy") && first == true)
        {
            first = false;
        }
    }


    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}