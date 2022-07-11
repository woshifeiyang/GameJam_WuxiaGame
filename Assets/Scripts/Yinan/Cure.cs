using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : FieldSkillBase
{
    public static bool cure = false;
   
    public void EnableCure()
    {
        cure = true;
    }

    
    public void CurePlayer(float distance)
    {
        if (cure)
        {
            if (distance < range)
            {
                if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth)
                    PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth/20;
                Debug.Log("治疗成功");
            }
            Debug.Log("超出范围");
        }
    }
}
