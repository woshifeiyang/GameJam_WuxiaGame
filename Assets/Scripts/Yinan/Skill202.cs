using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill202 : ScopeSkillBase
{
    private Rigidbody2D _rb;
    private int randomIndex;
    // Start is called before the first frame update
    public override void Start()
    {
        this.transform.localScale = new Vector3(range,range,1);
        //this.GetComponent<CircleCollider2D>().radius = range;
        List<GameObject> visibleenemies;
        visibleenemies = EnemyDetector.Instance.enemyList;
        if(visibleenemies.Count != 0)
        {
            randomIndex = Random.Range(0, visibleenemies.Count);
            _rb = GetComponent<Rigidbody2D>();
            _rb.transform.position = visibleenemies[randomIndex].transform.position;
        }
        
        
        Invoke("SelfDestory", skillTime);
        


    }

    // Update is called once per frame

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
