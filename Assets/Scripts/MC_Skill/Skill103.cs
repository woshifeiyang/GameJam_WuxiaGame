using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill103 : BulletSkillBase
{
    private Rigidbody2D _rb;

    public float rotationAngel;

    // Start is called before the first frame update
    public override void Start()
    {
        //伤害系数
        damage *= 1f;
        _rb = GetComponent<Rigidbody2D>();
        Vector3 vec = EnemyDetector.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position;
        vec.z = 0;
        float angle = Vector3.SignedAngle(Vector3.up, vec, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        _rb.AddForce(vec.normalized * speed, ForceMode2D.Force);
        // 持续时间结束时销毁自身
        Invoke("SelfDestory", skillTime);
        
        string assertPath = "Prefab/Skill/Bullet/103m";
        for (int i = 1; i < skillNum; i++)
        {
            float childOffsetAngle = i % 2 == 0 ? rotationAngel * (int)((i + 1) / 2) : -1 * rotationAngel * (int)((i + 1) / 2);
            Quaternion childRotation = Quaternion.Euler(0, 0, childOffsetAngle);
            Vector3 childVec = childRotation * vec;     // 最终的向量方向
            
            float childRotationValue = Vector3.SignedAngle(Vector3.up, childVec, Vector3.forward);
            Quaternion childRotationAngel = Quaternion.Euler(0, 0, childRotationValue);

            GameObject childSkill = (GameObject)Instantiate(Resources.Load(assertPath));
            Rigidbody2D rb = childSkill.GetComponent<Rigidbody2D>();
            childSkill.transform.position = transform.position;
            childSkill.transform.rotation = childRotationAngel;
            rb.AddForce(childVec.normalized * speed, ForceMode2D.Force);
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
        if (obj.gameObject.CompareTag("Enemy") || obj.gameObject.CompareTag("EliteEnemy") || obj.gameObject.CompareTag("Boss"))
        {
            --skillPene;
            if (skillPene == 0)
            {
                SelfDestory();
            }
        }
    }
}
