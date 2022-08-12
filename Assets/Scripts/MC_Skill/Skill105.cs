using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill105 : BulletSkillBase
{
    private float rotateSpeed;

    private Transform center;
    private float distance;
    private Vector3 dir;
    private float Damage;
    private float Speed;
    public override void Start()
    {
        center = GameObject.Find("Player").transform;
        transform.position = new Vector3(center.position.x , center.position.y + 1.9f, center.position.z);
        damage *= 1f;      
        distance = Vector3.Distance(transform.position, center.position);
        dir = transform.position - center.position;
        rotateSpeed = speed * 2f;
        for(int i =0; i < skillNum; i++)
        {
            GameObject SkillObj = (GameObject)Instantiate(Resources.Load("Prefab/Skill/Bullet/105m"));
            if( i%2==0)
            {
                //SkillObj.transform.Rotate(new Vector3(0, 0, -180));
            }
            //Debug.Log("rotation=" + SkillObj.transform.rotation.z);
            SkillObj.transform.position = new Vector3(center.position.x, transform.position.y + 0.9f*(i+1), center.position.z);
            
            
        }
        Invoke("SelfDestory", skillTime);
    }

    public override void Update()
    {
        //rotateSpeed = 1000f;
        transform.position = center.position + dir.normalized * distance;
        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        dir = transform.position - center.position;
    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }

}
