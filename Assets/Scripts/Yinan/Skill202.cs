using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill202 : ScopeSkillBase
{
    private Rigidbody2D _rb;
   
    // Start is called before the first frame update
    public override void Start()
    {
        this.transform.localScale = new Vector3(range,range,1);
        //this.GetComponent<CircleCollider2D>().radius = range;
        var visibleenemies = EnemyDetector.Instance.enemyList;
        var targets = visibleenemies.GetRandomElements(1);
        foreach (var e in targets)
        { 
            _rb = GetComponent<Rigidbody2D>();
            _rb.transform.position = e.transform.position;
            Invoke("SelfDestory", skillTime);
        }


    }

    // Update is called once per frame

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
