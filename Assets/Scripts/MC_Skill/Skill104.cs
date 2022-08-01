using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill104 : ScopeSkillBase
{
    public override void Start()
    {
        transform.position = EnemyDetector.Instance.GetNearestEnemyLoc();
        InvokeRepeating(nameof(MakeDamage), 0, 0.5f);
    }
    
    private void MakeDamage()
    {
        transform.localScale = new Vector3(range, range, 1f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range / 2);
        
        //OnDrawGizmos();
        Debug.Log("the num of collider array is:" + hitColliders.Length);
        foreach (var enemy in hitColliders)
        {
            if (enemy.gameObject != null)
            {
                enemy.gameObject.GetComponent<Monster>().GetDamaged(damage);
            }
        }
    }
    
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range / 2);
    }
}
