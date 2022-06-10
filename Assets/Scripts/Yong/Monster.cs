using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float _health;

    public float moveSpeed;

    private Rigidbody2D _rb;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + (_player.transform.position - transform.position).normalized * Time.fixedDeltaTime * moveSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        
    }
}
