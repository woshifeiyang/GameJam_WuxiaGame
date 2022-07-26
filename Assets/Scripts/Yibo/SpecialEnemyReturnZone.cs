using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyReturnZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EliteEnemy"))
        {
            StartCoroutine(moveEliteEnemyAfterSeconds(1f, other.gameObject));
        }
    }
    
    IEnumerator moveEliteEnemyAfterSeconds(float waitedTime, GameObject targetEnemy)
    {
        yield return new WaitForSeconds(waitedTime);
        
        Vector3 enemyTargetPosition = EnemySpawner.GetRandomPosition();
        targetEnemy.transform.position = enemyTargetPosition;
    }
}
