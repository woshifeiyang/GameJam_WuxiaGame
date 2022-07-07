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

    //private void UpgradeSkill()
    //{
    //    Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
    //    Cd = SkillObj.GetComponent<MultTargetSkillBase>().cd;
    //    SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
    //}
   

    //public void Strike()
    //{
    //    int num = EnemyDetector.Instance.enemyList.Count;
    //    List<GameObject> list;
    //    if (SkillNum > num) list = EnemyDetector.Instance.enemyList;
    //    else list = EnemyDetector.GetRandomElements(EnemyDetector.Instance.enemyList, SkillNum);
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        if (list[i])
    //        {
    //            _ = GameObject.Instantiate(SkillObj);
    //            SkillObj.transform.position = list[i].transform.position;
    //        }
    //    }
    //}

    // Update is called once per frame

}
