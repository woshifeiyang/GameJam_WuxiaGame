using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Scope,      //范围型技能     
    Bullet,     //弹道类技能
    Link,       //指向性技能
    Normal,     //常规技能
}

public class SkillBase 
{
    //为了解耦，一般不继承MonoBehavior类，将GameObject和Transform显示声明
    public int id;          //技能唯一id
    
    public string resName;  //资源路径
    
    public GameObject skillObj = null;
    
    public Transform skillTransform = null;
    
    public float skillTime;   //技能持续时间
    
    public SkillType mType;  //技能类型

    public float cd;        //技能冷却时间

    public float damage;    //技能伤害

    public bool isOver;     //声明周期是否结束
    
    public void CreateSkill()
    {
        //实例化对象
        skillObj = Resources.Load(resName) as GameObject;
        
        LoadSkillFinish();
    }
    public virtual void LoadSkillFinish()
    {
        //用来给子类重写这个方法，分别实现完成加载后的一些初始化工作
        isOver = false;    
    }
    //技能的更新类，用来更新位置、声明周期等,子类可重写
    public virtual void Update()
    {
        skillTime -= Time.deltaTime;
        //当技能的生命周期结束后，在SkillManager类中将它移除
        if (skillTime<0)
            isOver = true;
    }
}

//实现一个普通范围型AOE技能
public class ScopeSkill : SkillBase
{
    public GameObject point;//技能释放的一个位置标准点（用来定位这个技能的位置和角度）
    public ScopeSkill()
    {
        mType = SkillType.Scope;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        
        //SkillObj.transform.rotation = Quaternion.LookRotation(this.Point.tansform.rotation);
        //SkillObj.transform.rotation.position = this.Point.tansform.position;           
    }
}

public class BulletSkill : SkillBase
{
    public float speed;     //技能速度

    public int skillNum;    //技能数量

    public int skillPene;   //技能穿透数量
    
    public BulletSkill()
    {
        mType = SkillType.Scope;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        
        //SkillObj.transform.rotation = Quaternion.LookRotation(this.Point.tansform.rotation);
        //SkillObj.transform.rotation.position = this.Point.tansform.position;           
    }
}

public class LinkSkill : SkillBase
{
    public float speed;     //技能速度

    public int skillNum;    //技能数量

    public int skillPene;   //技能穿透数量
    
    public LinkSkill()
    {
        mType = SkillType.Scope;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        
        //SkillObj.transform.rotation = Quaternion.LookRotation(this.Point.tansform.rotation);
        //SkillObj.transform.rotation.position = this.Point.tansform.position;           
    }
}

public class NormalSkill : SkillBase
{
    public float speed;     //技能速度

    public int skillNum;    //技能数量

    public int skillPene;   //技能穿透数量
    
    public NormalSkill()
    {
        mType = SkillType.Scope;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        
        //SkillObj.transform.rotation = Quaternion.LookRotation(this.Point.tansform.rotation);
        //SkillObj.transform.rotation.position = this.Point.tansform.position;           
    }
}
