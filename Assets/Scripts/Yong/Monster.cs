using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float health;

    public float moveSpeed;

    private Rigidbody2D _rb;

    private GameObject _player;
    
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + (_player.transform.position - transform.position).normalized * Time.fixedDeltaTime * moveSpeed);
        if (transform.position.x - _player.transform.position.x > 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }else transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Skill"))
        {
            if (health - obj.gameObject.GetComponent<Skill>().damage > 0.0f)
            {
                health -= obj.gameObject.GetComponent<Skill>().damage;
                Debug.Log("Monster health = " + health);
            }
            else
            {
                SwitchAnim();
                moveSpeed = 0.0f;
                Invoke("SelfDestory", 1.0f);
            }

            if (obj.gameObject.GetComponent<Skill>().isDisappearable)
            {
                Destroy(obj.gameObject);
            }
        }
    }
    private void SwitchAnim()
    {
        _anim.SetBool("isDead", true);
    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
