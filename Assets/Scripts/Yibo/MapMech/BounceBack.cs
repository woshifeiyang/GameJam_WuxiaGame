using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    public float bounceForce = 1f;

    public GameObject player;

    public float bounceTime = 0.3f;

    public Animator anim;

    private Rigidbody2D rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().canMove = false;
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce( - col.contacts[0].normal * bounceForce);
            
            anim.SetBool("isTrigger", true);
            
            Invoke(nameof(StopBounce),bounceTime);
        }
    }

    void StopBounce()
    {
        player.GetComponent<PlayerController>().canMove = true;
        anim.SetBool("isTrigger", false);
    }
}
