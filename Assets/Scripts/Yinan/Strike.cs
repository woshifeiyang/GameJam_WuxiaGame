using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoSkillBase
{
    private GameObject SkillObj = null;
    private GameObject SkillObj2 = null;
    private GameObject SkillObj3 = null;
    private float Damage;
    private int SkillNum;
    private SkillBase skillBase;
    // Start is called before the first frame update
    void Start()
    {
        SkillObj = Resources.Load("Prefab/Skill/MultTarget/201") as GameObject;
        SkillObj2 = Resources.Load("Prefab/Skill/MultTarget/201m") as GameObject;
        SkillObj3 = Resources.Load("Prefab/Skill/Scope/202") as GameObject;
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
                obj.GetComponent<MultTargetSkillBase>().damage = Damage;               
            }
        }
    }
    public void Lightcall1()
    {
        int num = EnemyDetector.Instance.enemyList.Count;
        //Debug.Log("怪物数量"+num);
        if (num > 0)
        {
            List<GameObject> list = EnemyDetector.GetRandomElement(EnemyDetector.Instance.enemyList);
            GameObject obj = GameObject.Instantiate(SkillObj2);
            SkillObj2.transform.position = list[0].transform.position;
            obj.GetComponent<MultTargetSkillBase>().damage = Damage;
            //Invoke("Storm(SkillObj2.transform)", 0.3f);
            if (SkillManager.Instance.skillDic.TryGetValue(202, out skillBase))
            {
                if (skillBase != null)
                {
                    if (Random.value > 0.65)
                    {
                        damage = 150f;
                        GameObject obj1 = GameObject.Instantiate(SkillObj3);
                        SkillObj3.transform.position = SkillObj2.transform.position;
                    }
                   
                }
            }
        }
           
       
    }

    public void Storm(Transform transform1)
    {
        if (SkillManager.Instance.skillDic.TryGetValue(202, out skillBase))
        {
            if (skillBase != null)
            {
                damage = 100f;
                GameObject obj1 = GameObject.Instantiate(SkillObj3);
                SkillObj3.transform.position = transform1.position;
            }
        }
    }
}
