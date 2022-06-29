using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public Dictionary<int, SkillBase> skillDic = new Dictionary<int, SkillBase>();

    protected override void InitAwake()
    {
        base.InitAwake();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var skill in skillDic.Values)
        {
            skill.Update();
        }
    }
    
    //添加技能到技能表
    public void AddSkillObj(int id, SkillBase skill)
    {
        if (!skillDic.ContainsKey(id))
        {
            skillDic.Add(id, skill);
        }
    }

    //创建一个范围型AOE技能
    public SkillBase CreateScopeSkill(string resName, int id, GameObject parent)
    {
        //创建一个上面的ScopeSkill对象
        ScopeSkill scopeSkill = new ScopeSkill();
        scopeSkill.ResName = resName;
        scopeSkill.Point = parent;
        scopeSkill.ID = id;
        //取到资源路径后可以实例化这个技能对象了
        scopeSkill.CreateSkill();
        return scopeSkill;
    }
    
    //创建一个弹道类技能
    public SkillBase CreateBulletSkill(string resName, int id, GameObject parent)
    {
        //创建一个上面的BulletSkill对象
        BulletSkill bulletSkill = new BulletSkill();
        bulletSkill.ResName = resName;
        bulletSkill.ID = id;
        //取到资源路径后可以实例化这个技能对象了
        bulletSkill.CreateSkill();
        //以技能唯一Id作为key，加入我们的技能管理列表
        AddSkillObj(bulletSkill.ID, bulletSkill);
        return bulletSkill;
    }
    
    //创建一个追踪类技能
    public SkillBase CreateLinkSkill(string resName, int id, GameObject parent)
    {
        //创建一个上面的linkSkill对象
        LinkSkill linkSkill = new LinkSkill();
        linkSkill.ResName = resName;
        linkSkill.ID = id;
        //取到资源路径后可以实例化这个技能对象了
        linkSkill.CreateSkill();
        //以技能唯一Id作为key，加入我们的技能管理列表
        AddSkillObj(linkSkill.ID, linkSkill);
        return linkSkill;
    }
    
    //创建一个追踪类技能
    public SkillBase CreateNormalSkill(string resName, int id, GameObject parent)
    {
        //创建一个上面的NormalSkill对象
        NormalSkill normalSkill = new NormalSkill();
        normalSkill.ResName = resName;
        normalSkill.ID = id;
        //取到资源路径后可以实例化这个技能对象了
        normalSkill.CreateSkill();
        //以技能唯一Id作为key，加入我们的技能管理列表
        AddSkillObj(normalSkill.ID, normalSkill);
        return normalSkill;
    }
}
