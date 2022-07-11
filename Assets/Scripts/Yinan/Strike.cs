using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoSkillBase
{
    private GameObject SkillObj = null;
    private GameObject SkillObj2 = null;
    private float Damage;
    private int SkillNum;
    // Start is called before the first frame update
    void Start()
    {
        SkillObj = Resources.Load("Prefab/Skill/MultTarget/201") as GameObject;
        if (SkillObj != null)
        {
            Debug.Log("Skill init sucs");
        }
        SkillObj2 = Resources.Load("Prefab/Skill/MultTarget/201m") as GameObject;
        if(SkillObj2 != null)
        {
            Debug.Log("Skill2 init sucs");
        }
        UpgradeSkill();
    }

    // Update is called once per frame
    
    private void UpgradeSkill()
    {
        Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
        SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
        Damage = SkillObj2.GetComponent<MultTargetSkillBase>().damage;
        SkillNum = SkillObj2.GetComponent<MultTargetSkillBase>().skillNum;
    }
    public void Lightcall()
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
            }
        }
    }
    public void Lightcall1()
    {
        int num = EnemyDetector.Instance.enemyList.Count;
        Debug.Log("¹ÖÎïÊýÁ¿"+num);
        if (num > 0)
        {
            List<GameObject> list = EnemyDetector.GetRandomElement(EnemyDetector.Instance.enemyList);
            GameObject obj = GameObject.Instantiate(SkillObj2);
            SkillObj2.transform.position = list[0].transform.position;
            obj.GetComponent<MultTargetSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
        }
           
       
    }
}
