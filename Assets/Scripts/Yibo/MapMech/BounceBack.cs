using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    public float bounceForce = 1f;

    public GameObject player;

    public float bounceTime = 0.3f;

    private Rigidbody2D rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().canMove = false;
            rb.velocity = new Vector2(0f, 0f);
            //rb.AddForce( (col.GetContact(0). - player.transform.position) * bounceForce);
            /*Vector2 Direction = (this.transform.position - player.transform.position).normalized;
            Debug.Log("x: " + Direction.x + "Y: " + Direction.y);*/
            rb.AddForce( - col.contacts[0].normal * bounceForce);
            Invoke(nameof(StopBounce),bounceTime);
        }
    }

    void StopBounce()
    {
        player.GetComponent<PlayerController>().canMove = true;
    }
}
