using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaDamage : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().GetDamaged(damage);
        }
    }
}
