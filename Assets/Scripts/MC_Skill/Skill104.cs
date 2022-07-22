using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill104 : ScopeSkillBase
{
    public override void Start()
    {
        this.transform.position = EnemyDetector.Instance.GetNearestEnemyLoc();
    }


    private void ResetCollider()
    {
        this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;
        Invoke("SwitchCollider",0.02f);

    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
    private void SwordCall()
    {
        GameObject.Find("SkillManager").GetComponent<SwordEmi>().SwordCall1();        
    }
    private void SwitchCollider()
    {
        this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;
    }
}
