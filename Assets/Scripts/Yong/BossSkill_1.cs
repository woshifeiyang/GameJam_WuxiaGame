using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_1 : MonoBehaviour
{
    public float rotationAngel;

    public float cd;

    public int skillNum;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(ReleaseBarrage));
    }

    IEnumerator ReleaseBarrage()
    {
        while (true)
        {
            yield return new WaitForSeconds(cd);
            
            Vector3 vec = PlayerController.Instance.GetPlayerPosition() - transform.position;
            string assertPath = "Prefab/Enemy/Special/BossSkill/1";
            
            for (int i = 0; i < skillNum; i++)
            {
                GameObject skill = (GameObject)Instantiate(Resources.Load(assertPath));
                float childOffsetAngle = i % 2 == 0 ? rotationAngel * (int)((i + 1) / 2) : -1 * rotationAngel * (int)((i + 1) / 2);
                Quaternion childRotation = Quaternion.Euler(0, 0, childOffsetAngle);
                Vector3 childVec = childRotation * vec;     // 最终的向量方向
            
                float childRotationValue = Vector3.SignedAngle(Vector3.up, childVec, Vector3.forward);
                Quaternion childRotationAngel = Quaternion.Euler(0, 0, childRotationValue);
                
                Rigidbody2D rb = skill.GetComponent<Rigidbody2D>();
                skill.transform.position = transform.position;
                skill.transform.rotation = childRotationAngel;
                rb.AddForce(childVec.normalized * speed, ForceMode2D.Force);
            }
        }
    }
    
}
