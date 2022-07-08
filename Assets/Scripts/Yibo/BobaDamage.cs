using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaDamage : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("get touched by boba");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().GetDamaged(damage);
            Debug.Log("get damaged by boba");
        }
    }
}
