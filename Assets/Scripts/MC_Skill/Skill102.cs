
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill102 : BulletSkillBase
{
    private Rigidbody2D _rb;
    
    private Quaternion _rotation;

    private Vector3 _vec;

    // Start is called before the first frame update
    public override void Start()
    {
        //伤害系数
        damage *= 3f;
        _rb = GetComponent<Rigidbody2D>();
        _vec = EnemyDetector.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, _vec, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        _rotation = rotation;
        transform.rotation = _rotation;
        _rb.AddForce(_vec.normalized * speed, ForceMode2D.Force);
        // 持续时间结束时销毁自身
        Invoke("SelfDestory", skillTime);
        
        string assertPath = "Prefab/Skill/Bullet/102m";
        for (int i = 1; i < skillNum; i++)
        {
            float offset = i % 2 == 0 ? (i + 1) / 2 : -1 * ((i + 1) / 2);
            GameObject childSkill = (GameObject)Instantiate(Resources.Load(assertPath));
            
            childSkill.transform.position = new Vector3(transform.position.x + offset, transform.position.y + offset, 0);
            childSkill.transform.rotation = _rotation;
            childSkill.GetComponent<Rigidbody2D>().AddForce(_vec.normalized * speed, ForceMode2D.Force);
        }
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