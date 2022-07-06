using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Monster>().poolBelongTo == null)
            {
                Destroy(other.gameObject);
                Debug.Log("destroy out of pool");
            }
            else
            {
                EnemyObjectPool.EnemyObjectPoolInstance.PutObjectInPool(other.gameObject);
                Debug.Log("put object in pool");
            }

        }
    }
}
