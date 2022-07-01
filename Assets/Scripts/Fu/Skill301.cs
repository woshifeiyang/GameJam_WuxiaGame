using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill301 : FieldSkillBase
{
    [SerializeField]
    private Animator animator;
    private float skill301Timer;
    public override void Start()
    {
        this.GetComponent<CircleCollider2D>().radius = range;
        this.GetComponent<CircleCollider2D>().enabled = false;
        animator = this.GetComponent<Animator>();
    }

    public override void Update()
    {

        skill301Timer += Time.deltaTime;
        if(skill301Timer > cd)
        {
            skill301Timer = 0;

            this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;
            this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;

        }

    }
}
