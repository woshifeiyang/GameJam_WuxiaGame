using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill104 : ScopeSkillBase
{
    public override void Start()
    {
        transform.position = EnemyDetector.Instance.GetNearestEnemyLoc();
        Invoke(nameof(SelfDestory), skillTime);
    }
    
    public void MakeDamage()
    {
        transform.localScale = new Vector3(range, range, 1f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range / 2);
 
        Debug.Log("the num of collider array is:" + hitColliders.Length);
        foreach (var enemy in hitColliders)
        {
            if (enemy && enemy.gameObject.CompareTag("Enemy"))
            {
                enemy.gameObject.GetComponent<Monster>().GetDamaged(damage);
            }
        }
    }
    
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
