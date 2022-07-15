using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill104 : FieldSkillBase
{
    
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {

    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
    private void SwordCall()
    {
        GameObject.Find("SkillManager").GetComponent<SwordEmi>().SwordCall1();
        Debug.Log("Ω£’Û’ŸªΩ");
    }
}
