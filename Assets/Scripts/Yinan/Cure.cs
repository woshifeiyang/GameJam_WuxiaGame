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
                {
                    if(PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth*0.4f){
                        PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth * 0.03f;
                    }
                    else if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth * 0.2f)
                    {
                        PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth * 0.05f;
                    }
                    else
                    {
                        PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth *0.01f;
                    }
                    
                }
                    
                //Debug.Log("cure sucs&distace=" + distance);
            }
        }
    }
}
