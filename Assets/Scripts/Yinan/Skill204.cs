using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill204 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject SkillObj = null;
    private string ResName;
    //201
    private float Damage;
    private float Cd;
    private int SkillNum;
    void Start()
    {
        SkillObj = Resources.Load("Prefab/201") as GameObject;
        UpgradeSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpgradeSkill()
    {
        Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
        Cd = SkillObj.GetComponent<MultTargetSkillBase>().cd;
        SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
    }
}
