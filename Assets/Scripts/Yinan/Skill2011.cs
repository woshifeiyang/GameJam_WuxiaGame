using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2011 : MultTargetSkillBase
{
   
    public override void Start()
    {
        Invoke("SelfDestory", 0.3f);
    }

    
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
