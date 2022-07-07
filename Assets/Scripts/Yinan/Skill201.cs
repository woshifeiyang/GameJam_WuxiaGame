using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill201 : MultTargetSkillBase
{
    //public delegate void DouStk();
    //public static Strike strike;
    //private Rigidbody2D _rb;
    //投射物数量
    //public int amount = 4;
    // Start is called before the first frame update
    public override void Start()
    {
        if (Random.value > 0.5)
        {
            
            Debug.Log("随机完成");
            GameObject.Find("SkillManager").GetComponent<Strike>().Lightcall1();
        }
        //_rb = GetComponent<Rigidbody2D>();
        //_rb.transform.position = skillobj.transform.position;
        Invoke("SelfDestory", 0.3f);
    }

    // Update is called once per frame
    public override void Update()
    {
       

    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }

    
    
}
