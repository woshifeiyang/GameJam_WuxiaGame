using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill301 : FieldSkillBase
{
    public float skill301Timer;

    public float skillLastTime;

    private CircleCollider2D _cc;
    public override void Start()
    {
        _cc = this.GetComponent<CircleCollider2D>();
        //_cc.radius = range;
        _cc.enabled = false;
        
    }

    public override void Update()
    {
        this.transform.localScale = new Vector3(range, range, 1f);
        skill301Timer += Time.deltaTime;
        //_cc.radius = range;
        if(skill301Timer > cd)
        {
            damage = 0.3f * PlayerController.Instance.maxHealth;
            skill301Timer = 0;

            this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;

            StartCoroutine("SkillLastSeconds");
        }
    }

    IEnumerator SkillLastSeconds()
    {
        yield return new WaitForSeconds(skillLastTime);
        this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;
    }
}
