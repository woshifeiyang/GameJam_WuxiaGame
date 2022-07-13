using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill304 : FieldSkillBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        GameObject.Find("SkillManager").GetComponent<Karma>().EnableKarma();
    }

    // Update is called once per frame
   
}
