using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Scope,          //范围型技能     
    Bullet,         //弹道类技能
    Field,          //领域技能
    MultTarget,     //多目标单体技能
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

// 范围型技能：直接在目标点按照技能cd生成技能
public class ScopeSkill : SkillBase
{
    public float Damage;
    public float Cd;
    public float Range;
    
    public ScopeSkill()
    {
        MType = SkillType.Scope;
    }
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();
        Damage = SkillObj.GetComponent<ScopeSkillBase>().damage;
        Cd = SkillObj.GetComponent<ScopeSkillBase>().cd;
        Range = SkillObj.GetComponent<ScopeSkillBase>().range;
    }
    public override void Update()
    {
        base.Update();
        Timer += (Time.deltaTime);
        if (Timer >= Cd)
        {
            //初始化技能生成的属性
            GameObject obj = GameObject.Instantiate(SkillObj);
            obj.GetComponent<ScopeSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
            obj.GetComponent<ScopeSkillBase>().range = Range + PlayerController.Instance.GetPlayerSkillRange();
            obj.GetComponent<ScopeSkillBase>().cd = Cd * PlayerController.Instance.GetPlayerSkillCd();
            Timer = 0;
        }
    }
}
// 弹道类技能：以一个对象为目标，按照技能cd从玩家位置生成一个技能
public class BulletSkill : SkillBase
{
    public float Speed;
    public float Damage;
    public float Cd;
    public int SkillNum;

    public BulletSkill()
    {
        MType = SkillType.Bullet;
    }
    public override void LoadSkillFinish()
    {
        // 记录技能的初始属性
        base.LoadSkillFinish();
        Speed = SkillObj.GetComponent<BulletSkillBase>().speed;
        Damage = SkillObj.GetComponent<BulletSkillBase>().damage;
        Cd = SkillObj.GetComponent<BulletSkillBase>().cd;
        SkillNum = SkillObj.GetComponent<BulletSkillBase>().skillNum;
    }
    
    public override void Update()
    {
        base.Update();
        Timer += (Time.deltaTime);
        if (Timer >= Cd)
        {
            //初始化技能生成的属性
            GameObject obj = GameObject.Instantiate(SkillObj);
            obj.transform.position = PlayerController.Instance.GetPlayerPosition();
            obj.GetComponent<BulletSkillBase>().speed = Speed + PlayerController.Instance.GetPlayerSkillSpeed();
            obj.GetComponent<BulletSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
            obj.GetComponent<BulletSkillBase>().skillNum = SkillNum + PlayerController.Instance.GetPlayerProjectileNum();
            obj.GetComponent<BulletSkillBase>().cd = Cd * PlayerController.Instance.GetPlayerSkillCd();
            Timer = 0;
        }
    }
}
// 领域类技能：以玩家位置为中心生成一个永久的对象
public class FieldSkill : SkillBase
{
    public GameObject Skill;
    public float Damage;
    public float Cd;
    public float Range;
    
    public FieldSkill()
    {
        MType = SkillType.Field;
    }
    
    public override void LoadSkillFinish()
    {
        base.LoadSkillFinish();
        //初始化技能生成的属性
        Skill = GameObject.Instantiate(SkillObj);
        Damage = Skill.GetComponent<FieldSkillBase>().damage;
        Range = Skill.GetComponent<FieldSkillBase>().range;
        Cd = Skill.GetComponent<FieldSkillBase>().cd;
    }
    public override void Update()
    {
        base.Update();
        Skill.transform.position = PlayerController.Instance.GetPlayerPosition();
        Skill.GetComponent<FieldSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
        Skill.GetComponent<FieldSkillBase>().range = Range + PlayerController.Instance.GetPlayerSkillRange();
        Skill.GetComponent<FieldSkillBase>().cd = Cd * PlayerController.Instance.GetPlayerSkillCd();
    }
}
// 多目标单体技能： 随机选取屏幕内预定数量的怪物生成多个技能造成单体伤害
public class MultTargetSkill : SkillBase
{
    public float Damage;
    public float Cd;
    public int SkillNum;
    
    public MultTargetSkill()
    {
        MType = SkillType.MultTarget;
    }
    
    public override void LoadSkillFinish()
    {
        //设置这个技能生成的位置（如鼠标位置、指向性范围一定距离处等）
        base.LoadSkillFinish();

        Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
        Cd = SkillObj.GetComponent<MultTargetSkillBase>().cd;
        SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
    }
    public override void Update()
    {
        base.Update();
        Timer += (Time.deltaTime);
        if (Timer >= Cd)
        {
            int num = EnemyDetector.Instance.enemyList.Count;
            List<GameObject> list;
            if (SkillNum > num) list = EnemyDetector.Instance.enemyList;
            else list = EnemyDetector.GetRandomElements(EnemyDetector.Instance.enemyList, SkillNum);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i])
                {
                    GameObject obj = GameObject.Instantiate(SkillObj);
                    SkillObj.transform.position = list[i].transform.position;
                    obj.GetComponent<MultTargetSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
                    obj.GetComponent<MultTargetSkillBase>().cd = Cd * PlayerController.Instance.GetPlayerSkillCd();
                }
            }
            Timer = 0;
        }
    }
}
