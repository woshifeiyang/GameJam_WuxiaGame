using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill303AnimEvent : FieldSkillBase
{

    [SerializeField]
    private float colliderLastTime;
    // Start is called before the first frame update
   

    private void ColliderOpen()
    {
        //ÉËº¦ÏµÊýºÍ·¶Î§
        this.transform.localScale *= ((range*0.25f)+1); 
        damage = 0.5f * PlayerController.Instance.curHealth;
        this.GetComponent<CircleCollider2D>().enabled = true;
        //Time.timeScale = 0;
        Debug.Log(this.GetComponent<CircleCollider2D>().enabled);
        Invoke(nameof(CloseCollider), colliderLastTime);
    }
    
    private void CloseCollider()
    {
        
        this.GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log(this.GetComponent<CircleCollider2D>().enabled);
    }
}
