using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill203 : FieldSkillBase
{
    private GameObject SkillObj = null;
    private string ResName;
    //201
    private float Damage;
    private float Cd;
    private int SkillNum;
    // Start is called before the first frame update
    public override void Start()
    {
        SkillObj = Resources.Load("Prefab/201") as GameObject;
        UpgradeSkill();
    }
    public override void Update()
    {
        base.Update();
        if (PlayerController.attackByEnemy)
        {
            int num = EnemyDetector.Instance.enemyList.Count;
            List<GameObject> list;
            if (SkillNum > num) list = EnemyDetector.Instance.enemyList;
            else list = EnemyDetector.GetRandomElements(EnemyDetector.Instance.enemyList, SkillNum);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i])
                {
                    _ = GameObject.Instantiate(SkillObj);
                    SkillObj.transform.position = list[i].transform.position;
                }
            }
            PlayerController.attackByEnemy = false;
        }

    }

    private void UpgradeSkill()
    {
        Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
        Cd = SkillObj.GetComponent<MultTargetSkillBase>().cd;
        SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
    }

    // Update is called once per frame

}
