using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEmi : MonoSkillBase
{
    private GameObject SkillObj = null;
    private GameObject Sword = null;
    private float Damage;
    private int SkillNum;
    // Start is called before the first frame update
     void Start()
    {
        Sword = Resources.Load("Prefab/Skill/Field/104s1") as GameObject;
        SkillObj = Resources.Load("Prefab/Skill/MultTarget/104") as GameObject;
        if (SkillObj != null)
        {
            Debug.Log("剑阵初始化");
        }
        
    }

        // Update is called once per frame
    
    private void UpgradeSkill()
    {
       Damage = SkillObj.GetComponent<MultTargetSkillBase>().damage;
       SkillNum = SkillObj.GetComponent<MultTargetSkillBase>().skillNum;
           
    }
    public void SwordCall()
    {
        int num = EnemyDetector.Instance.enemyList.Count;
        List<GameObject> list;
        if (SkillNum > num) list = EnemyDetector.Instance.enemyList;
        else list = EnemyDetector.GetRandomElements(EnemyDetector.Instance.enemyList, SkillNum);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i])
            {
                Debug.Log("剑阵实例化");
                GameObject obj = GameObject.Instantiate(SkillObj);
                SkillObj.transform.position = list[i].transform.position;
                obj.GetComponent<MultTargetSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
            }
        }
    }
    public void SwordCall1()
    {
        int num = EnemyDetector.Instance.enemyList.Count;
        Debug.Log("怪物数量" + num);
        if (num > 0)
        {
            List<GameObject> list = EnemyDetector.GetRandomElement(EnemyDetector.Instance.enemyList);
            GameObject obj = GameObject.Instantiate(SkillObj);
            SkillObj.transform.position = list[0].transform.position;
            obj.GetComponent<MultTargetSkillBase>().damage = Damage + PlayerController.Instance.GetPlayerAttack();
        }


    }
    public void SwordDiscard()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = GameObject.Instantiate(Sword);
            obj.transform.position = GameObject.Find("Player").transform.position;
            Invoke("SelfDestory(obj)", 0.5f);
        }
        
    }
    private void SelfDestory(GameObject obj)
    {
        Destroy(obj);
    }
}
