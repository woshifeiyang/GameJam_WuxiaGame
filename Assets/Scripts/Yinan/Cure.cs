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
        if (cure)
        {
            if (distance < (range+2)*0.5f)
            {
                if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth)
                    PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth/100;
            }
        }
    }
}
