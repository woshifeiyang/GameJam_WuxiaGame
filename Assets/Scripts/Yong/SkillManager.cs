using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public Dictionary<int, SkillBase> skillDic = new Dictionary<int, SkillBase>();
    public List<SkillBase> m_OverSkillList = new List<SkillBase>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    //添加技能到表表
    public void AddSkillObj(int id, SkillBase skill)
    {
        if (!skillDic.ContainsKey(id))
        {
            skillDic.Add(id, skill);
        }
    }
    
    //创建一个范围型AOE技能
    public SkillBase CreateScopeSkill(string resName, GameObject parent)
    {
        //创建一个上面的ScopeSkill对象
        ScopeSkill scopeSkill = new ScopeSkill();
        scopeSkill.resName = resName;
        scopeSkill.point = parent;
        //取到资源路径后可以实例化这个技能对象了
        scopeSkill.CreateSkill();
        //以技能唯一Id作为key，加入我们的技能管理列表
        AddSkillObj(scopeSkill.id, scopeSkill);
        return scopeSkill;
    }
    
    //创建一个弹道类技能
    public SkillBase CreateBulletSkill(string resName, GameObject parent)
    {
        //创建一个上面的ScopeSkill对象
        BulletSkill bulletSkill = new BulletSkill();
        bulletSkill.resName = resName;
        //取到资源路径后可以实例化这个技能对象了
        bulletSkill.CreateSkill();
        //以技能唯一Id作为key，加入我们的技能管理列表
        AddSkillObj(bulletSkill.id, bulletSkill);
        return bulletSkill;
    }
    
    //创建一个弹道类技能
    public SkillBase CreateLinkSkill(string resName, GameObject parent)
    {
        //创建一个上面的ScopeSkill对象
        LinkSkill linkSkill = new LinkSkill();
        linkSkill.resName = resName;
        //取到资源路径后可以实例化这个技能对象了
        linkSkill.CreateSkill();
        //以技能唯一Id作为key，加入我们的技能管理列表
        AddSkillObj(linkSkill.id, linkSkill);
        return linkSkill;
    }
}
