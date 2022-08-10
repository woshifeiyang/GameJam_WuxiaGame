using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill105 : BulletSkillBase
{
    private float rotateSpeed;

    private Transform center;
    private float distance;
    private Vector3 dir;
    public override void Start()
    {
        cd = 9999;
        center = GameObject.Find("Player").transform;
        transform.position = new Vector3(center.position.x , center.position.y + 3.2f, center.position.z);
        damage *= 2f;      
        distance = Vector3.Distance(transform.position, center.position);
        dir = transform.position - center.position;
        Invoke("SelfDestory", skillTime);
    }

    public override void Update()
    {
        base.Update();
        rotateSpeed = speed * 10f;
        transform.position = center.position + dir.normalized * distance;
        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        dir = transform.position - center.position;
    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }

}
