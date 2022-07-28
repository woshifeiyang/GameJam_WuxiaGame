using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill102M : BulletSkillBase
{
    // Start is called before the first frame update
    void Start()
    {
        SkillBase skillBase;
        if (SkillManager.Instance.skillDic.TryGetValue(102, out skillBase))
        {
            if (skillBase != null)
            {
                damage = skillBase.SkillObj.GetComponent<Skill102>().damage;
                skillTime = skillBase.SkillObj.GetComponent<Skill102>().skillTime;
                skillPene = skillBase.SkillObj.GetComponent<Skill102>().skillPene;
                transform.localScale = skillBase.SkillObj.transform.localScale;
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
