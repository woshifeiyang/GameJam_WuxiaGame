using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill202 : ScopeSkillBase
{
    private Rigidbody2D _rb;
    private int targetnum;
    // Start is called before the first frame update
    public override void Start()
    {
        this.transform.localScale = new Vector3(range,range,1);
        //this.GetComponent<CircleCollider2D>().radius = range;
        var visibleenemies = EnemyDetector.Instance.enemyList;
        if(visibleenemies.Count != 0)
        {
            targetnum = Mathf.Min(visibleenemies.Count, 1);
        }
        var targets = visibleenemies.GetRandomElements(targetnum);
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
