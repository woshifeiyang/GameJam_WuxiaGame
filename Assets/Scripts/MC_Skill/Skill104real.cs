using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill104real : MultTargetSkillBase
{
   private void ColliderOpen()
    {
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
    private void selfDestory()
    {
        Destroy(gameObject);
    }
}
