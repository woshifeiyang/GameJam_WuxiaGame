using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkillBase : MonoBehaviour
{
    public float speed;           // 技能速度

    public float damage;         // 技能伤害

    public float cd;             // 技能释放CD

    public int skillNum;        // 技能数量

    public int skillPene;       // 技能最大穿透数量，当穿透数量为0时销毁对象
    
    public float skillTime;     //技能持续时间
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
