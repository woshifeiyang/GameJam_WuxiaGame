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
            if (distance < 0.5f*(range+2.3))
            {
                if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth)
                    PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth/100;
                Debug.Log("cure sucs&distace=" + distance);
            }
        }
    }
}
