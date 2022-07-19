using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill203 : FieldSkillBase
{
 
    public override void Start()
    {
        _ = GameObject.Find("Player").AddComponent<AntiThunder>();
        
    }
    

    

}
