using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill204 : FieldSkillBase
{
    // Start is called before the first frame update
    private GameObject SkillObj = null;
    private string ResName;
    private delegate void EnemyKills();
    private EnemyKills _enemyKills;
    //201
    private float Damage;
    private float Cd;
    private int SkillNum;

    public override void Start()
    {

        SkillObj = Resources.Load("Prefab/201") as GameObject;
        UpgradeSkill();
        _enemyKills = new EnemyKills(DoubleStrike);

    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    private void UpgradeSkill()
    {
        Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
        Cd = SkillObj.GetComponent<MultTargetSkillBase>().cd;
        SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
    }
    public void DoubleStrike()
    {

    }
}
