using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Animator _anim;
    
    private Vector2 _movement;

    public Transform skill;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        InvokeRepeating("SpawnSkill", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.x != 0)
        {
            transform.localScale = new Vector3(_movement.x, 1, 1);
        }
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * speed * Time.fixedDeltaTime);
    }

    private void SwitchAnim()
    {
        _anim.SetFloat("speed", _movement.magnitude);
    }

    void SpawnSkill()
    {
        
        Instantiate(skill, new Vector3(transform.position.x, transform.position.y + 1.0f), transform.rotation);
    }
}
