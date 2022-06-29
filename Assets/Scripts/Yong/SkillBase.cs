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
    public int ID;          //技能唯一id
    
    public string ResName;  //资源路径
    
    public GameObject SkillObj = null;
    
    public Transform SkillTransform = null;

    public SkillType MType;  //技能类型
    
    public float Timer = 0.0f;  // 定时器

    public void CreateSkill()
    {
        //实例化对象
        SkillObj = Resources.Load(ResName) as GameObject;
        //以技能唯一Id作为key，加入我们的技能管理列表
        SkillManager.Instance.AddSkillObj(ID, this);
        LoadSkillFinish();
    }
    public virtual void LoadSkillFinish()
    {
    }
    //技能的更新类，用来更新位置、声明周期等,子类可重写
    public virtual void Update()
    {
    }
}

// 范围型技能：以Point为目标点，直接在目标点按照技能cd生成技能
public class ScopeSkill : SkillBase
{
    //技能释放的一个位置标准点（用来定位这个技能的位置和角度）
    public GameObject Point;
    public ScopeSkill()
    {
        MType = SkillType.Scope;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();    
    }
    public override void Update()
    {
        base.Update();
        Timer += (Time.deltaTime);
        if (Timer >= SkillObj.GetComponent<ScopeSkillBase>().cd)
        {
            GameObject obj = GameObject.Instantiate(SkillObj);
            obj.transform.position = Point.transform.position;
            Timer = 0;
        }
    }
}
// 弹道类技能：以一个对象为目标，按照技能cd从玩家位置生成一个技能
public class BulletSkill : SkillBase
{
    public BulletSkill()
    {
        MType = SkillType.Bullet;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        
    }
    
    public override void Update()
    {
        base.Update();
        Timer += (Time.deltaTime);
        if (Timer >= SkillObj.GetComponent<BulletSkillBase>().cd)
        {
            GameObject obj = GameObject.Instantiate(SkillObj);
            obj.transform.position = PlayerController.Instance.GetPlayerPosition();
            Timer = 0;
        }
    }
}
// 领域类技能：以玩家位置为中心生成一个永久的对象
public class FieldSkill : SkillBase
{
    public FieldSkill()
    {
        MType = SkillType.Link;
    }
    
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        
        GameObject obj = GameObject.Instantiate(SkillObj);
        obj.transform.position = PlayerController.Instance.GetPlayerPosition();
    }
    public override void Update()
    {
        base.Update();
        
    }
}

public class NormalSkill : SkillBase
{
    public NormalSkill()
    {
        MType = SkillType.Normal;
    }
    
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();

        //SkillObj.transform.rotation = Quaternion.LookRotation(this.Point.tansform.rotation);
        //SkillObj.transform.rotation.position = this.Point.tansform.position;           
    }
    public override void Update()
    {
        base.Update();
    }
}
