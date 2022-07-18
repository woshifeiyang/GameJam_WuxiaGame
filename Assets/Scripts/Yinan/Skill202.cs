using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill202 : ScopeSkillBase
{
    private Rigidbody2D _rb;
    private int randomIndex;
    private bool triggered = false;
    private Animator animator;
    // Start is called before the first frame update
    public override void Start()
    {
        animator = this.GetComponent<Animator>();
        this.transform.localScale = new Vector3(3+range*1.5f,3+range*1.5f,1);
        //this.GetComponent<CircleCollider2D>().radius = range;
        List<GameObject> visibleenemies;
        visibleenemies = EnemyDetector.Instance.enemyList;
        if(visibleenemies.Count != 0)
        {
            randomIndex = Random.Range(0, visibleenemies.Count);
            _rb = GetComponent<Rigidbody2D>();
            _rb.transform.position = visibleenemies[randomIndex].transform.position;
            _rb.transform.position = _rb.transform.position + (Random.insideUnitSphere);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.CompareTag("Player") && triggered == false)
        {
            triggered = true;
            animator.SetTrigger("lightstorm");

        }
    }
    private void ColliderOpen()
    {
        
        this.GetComponent<CircleCollider2D>().enabled = true;
        //Time.timeScale = 0;
        //Invoke("SelfDestory", 0.6f);
        
    }

    // Update is called once per frame

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
