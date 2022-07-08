using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill201 : MultTargetSkillBase
{
    
    public override void Start()
    {
        bool dou = GameObject.Find("SkillManager").GetComponent<DouStk>().GetDou();
        Debug.Log("dou" + dou);
        if (dou)
        {
            
            if (Random.value > 0.5)
            {
                Debug.Log("Ëæ»úÍê³É");
                Invoke("Lightcall1", 0.3f);
            }
        }
        
        Invoke("SelfDestory", 0.3f);
    }

    private void Lightcall1()
    {
        GameObject.Find("SkillManager").GetComponent<Strike>().Lightcall1();
    }

   
    private void SelfDestory()
    {
        Destroy(gameObject);
    }

    
    
}
