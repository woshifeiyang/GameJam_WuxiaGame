using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSkillBase : MonoBehaviour
{
    public int id;                // 技能唯一ID

    public float damage;         // 技能伤害

    public float cd;             // 技能释放CD
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class BulletSkillBase : MonoSkillBase
{
    public float speed;           // 技能速度

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

public class ScopeSkillBase : MonoSkillBase
{
    public float skillTime;     // 技能持续时间

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

public class FieldSkillBase : MonoSkillBase
{
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

public class MultTargetSkillBase : MonoSkillBase
{
    public int skillNum;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
