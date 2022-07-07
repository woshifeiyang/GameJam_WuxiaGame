using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiThunder : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
       
    }

    // Update is called once per frame
    
    void OnCollisionEnter2D()
    {
       GameObject.Find("SkillManager").GetComponent<Strike>().Lightcall();
    }
}
