using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill203 : FieldSkillBase
{
    private GameObject obj = null;
    public void Awake()
    {
        obj = GameObject.Find("Player");
    }
    
    public override void Start()
    {
         obj.AddComponent<AntiThunder>();
        //SkillObj = Resources.Load("Prefab/201") as GameObject;
        //UpgradeSkill();
    }
    public override void Update()
    {
        base.Update();
        
    }

    

}
