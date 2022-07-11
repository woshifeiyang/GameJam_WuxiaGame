using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill103M : BulletSkillBase
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestory", skillTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy"))
        {
            --skillPene;
            if (skillPene == 0)
            {
                SelfDestory();
            }
        }
    }
}
