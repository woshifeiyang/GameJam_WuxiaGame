using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSkillBase : MonoBehaviour
{
    public int id;                // 技能唯一ID
    
    public float damage;         // 技能伤害

    public float cd;             // 技能释放CD

    public float range;         // 技能范围

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
