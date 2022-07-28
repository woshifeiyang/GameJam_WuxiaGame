using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill101M : BulletSkillBase
{
    // Start is called before the first frame update
    void Start()
    {
        SkillBase skillBase;
        if (SkillManager.Instance.skillDic.TryGetValue(101, out skillBase))
        {
            if (skillBase != null)
            {
                damage = skillBase.SkillObj.GetComponent<Skill101>().damage;
                skillTime = skillBase.SkillObj.GetComponent<Skill101>().skillTime;
                skillPene = skillBase.SkillObj.GetComponent<Skill101>().skillPene;
            }
        }
        
        Invoke("SelfDestory", skillTime);
    }

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy") || obj.gameObject.CompareTag("EliteEnemy") || obj.gameObject.CompareTag("Boss"))
        {
            --skillPene;
            if (skillPene == 0)
            {
                SelfDestory();
            }
        }
    }
}
