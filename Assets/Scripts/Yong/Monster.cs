using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float health;

    public float moveSpeed;

    public float damage;

    private float _moveSpeed;
    
    public bool isDead;
    //怪物是否能够移动
    public bool canMove; 

    public string poolBelongTo;

    private Rigidbody2D _rb;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        
        SetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 playerPosition = PlayerController.PlayerControllerInstance.GetPlayerPosition();
        if (canMove)
        {
            _rb.MovePosition(transform.position + (playerPosition - transform.position).normalized * Time.fixedDeltaTime * _moveSpeed );
        }
        if (transform.position.x - playerPosition.x > 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }else transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Skill") && isDead == false)
        {
            if (health - obj.gameObject.GetComponent<Skill>().damage > 0.0f)
            {
                health -= obj.gameObject.GetComponent<Skill>().damage;
                Debug.Log("Monster health = " + health);
            }
            else
            {
                PlayerController.PlayerControllerInstance.IncreaseExperience();
                
                isDead = true;
                _moveSpeed = 0.0f;
                SwitchAnim();
                Invoke(nameof(PutObjectInPool), 1.0f);
            }

            if (obj.gameObject.GetComponent<Skill>().isDisappearable)
            {
                Destroy(obj.gameObject);
            }
        }
    }

    public void SetMoveSpeed()
    {
        _moveSpeed = moveSpeed;
    }
    private void SwitchAnim()
    {
        _anim.SetBool("isDead", true);
    }

    private void PutObjectInPool()
    {
        EnemyObjectPool.EnemyObjectPoolInstance.PutObjectInPool(this.gameObject);
    }

}
