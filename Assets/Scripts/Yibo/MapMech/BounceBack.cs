using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    public float bounceForce = 1f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.rigidbody.AddForce(col.contacts[0].normal * bounceForce);
        }
    }
}
