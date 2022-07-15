using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiThunder : MonoBehaviour
{
    public void Start()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && col.gameObject.GetComponent<Monster>().isDead == false)
        {
            GameObject.Find("SkillManager").GetComponent<Strike>().Lightcall();
        }
    }
}
