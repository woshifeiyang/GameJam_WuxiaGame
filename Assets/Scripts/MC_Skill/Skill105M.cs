using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill105M : BulletSkillBase
{
    private float rotateSpeed;

    private Transform center;
    private float distance;
    private Vector3 dir;
    private float Damage;
    private float rotatationDir;
    public override void Start()
    {
        SkillBase skillBase;
        SkillManager.Instance.skillDic.TryGetValue(105, out skillBase);
        center = GameObject.Find("Player").transform;
        damage *= 1f;
        distance = Vector3.Distance(transform.position, center.position);
        dir = transform.position - center.position;
        damage = skillBase.SkillObj.GetComponent<Skill105>().damage + PlayerController.Instance.GetPlayerAttack();
        speed = skillBase.SkillObj.GetComponent<Skill105>().speed + PlayerController.Instance.GetPlayerSkillSpeed();
        skillTime = GetComponent<BulletSkillBase>().skillTime;
        rotateSpeed = speed * 2f;
        rotatationDir = transform.rotation.z ;
        Debug.Log("rotation=" + transform.rotation.z);
        if (rotatationDir > 0)
        {
            rotatationDir = 1;
        }
        else
            rotatationDir = -1;
        
        Invoke("SelfDestory", skillTime);
    }

    // Update is called once per frame
    public override void Update()
    {

        Debug.Log("105m speed=" + speed);
        transform.position = center.position + dir.normalized * distance;
        transform.RotateAround(center.position, new Vector3(0,0,1), rotateSpeed * Time.deltaTime);
        dir = transform.position - center.position;
    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
