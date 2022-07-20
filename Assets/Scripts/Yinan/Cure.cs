using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : FieldSkillBase
{
    public bool cure = false;
   
    public void EnableCure()
    {
        cure = true;
    }

    
    public void CurePlayer(float distance)
    {
        if (cure&GameObject.Find("301").GetComponent<CircleCollider2D>())
        {
            if (distance < GameObject.Find("301").GetComponent<CircleCollider2D>().radius)
            {
                if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth)
                    PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth/100;
            }
        }
    }
}
