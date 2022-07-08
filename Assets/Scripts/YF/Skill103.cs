
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
        
        _rb = GetComponent<Rigidbody2D>();
        Vector3 vec = EnemyDetector.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position;
        vec.z = 0;
        float angle = Vector3.SignedAngle(Vector3.up, vec, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        transform.position = PlayerController.Instance.transform.position + new Vector3(0,1,0);
        _rb.AddForce(vec.normalized * speed, ForceMode2D.Force);
        // 持续时间结束时销毁自身
        Invoke("SelfDestory", skillTime);
        
        string assertPath = "Prefab/Skill/Bullet/103m";
        for (int i = 1, j = 1; i < skillNum; i++)
        {
            float childAngle = i % 2 == 0 ? rotationAngel * i : -1 * rotationAngel * i;
            
            GameObject childSkill = (GameObject)Instantiate(Resources.Load(assertPath));
                
        }
        

        /*if (skillNum > 1)
        {
            for (int i = 1; i < skillNum; i++)
            {
                
            }
            Vector3 vec = EnemyDetector.Instance.GetNearestEnemyLoc() - PlayerController.Instance.transform.position;
            Quaternion offset = Quaternion.Euler(0, 0, rotationAngel);
            Vector3 Normal = offset * vec;
            Normal.z = 0;
            float angle = Vector3.SignedAngle(Vector3.up, Normal, Vector3.forward);
            Quaternion picRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = picRotation;
            _rb.AddForce((Normal).normalized * speed, ForceMode2D.Force);
            // 持续时间结束时销毁自身
            Invoke("SelfDestory", skillTime);
        }*/
        
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
