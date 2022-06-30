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

    public DamagePopupManager DamagePopupManager;

    private Rigidbody2D _rb;

    private Animator _anim;
    
    private Vector3 importedLocalScale;

    private void OnEnable()
    {
        SetAlive();
    }
    // 如果怪物在摄像机内可见，添加进敌人探测器的可视怪物列表中
    private void OnBecameVisible()
    {
        EnemyDetector.Instance.enemyList.Add(gameObject);
    }

    private void OnBecameInvisible()
    {
        EnemyDetector.Instance.enemyList.Remove(gameObject);
    }

    void Start()
    {
        // DamagePopupManager = GameObject.FindWithTag("DamagePopupManager").GetComponent<DamagePopupManager>();
        
        importedLocalScale = transform.localScale;
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
        Vector3 playerPosition = PlayerController.Instance.GetPlayerPosition();
        if (canMove)
        {
            _rb.MovePosition(transform.position + (playerPosition - transform.position).normalized * Time.fixedDeltaTime * _moveSpeed );
        }

        if (transform.position.x - playerPosition.x > 0)
        {
            transform.localScale = new Vector3(-1f * importedLocalScale.x, importedLocalScale.y, importedLocalScale.y);
        }
        else transform.localScale = importedLocalScale;
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Skill") && isDead == false)
        {
            GetDamaged(obj.gameObject);
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

    public void SetAlive()
    {
        isDead = false;
        SetMoveSpeed();
        GetComponent<Collider2D>().isTrigger = false;
    }

    public void SetDead()
    {
        isDead = true;
        EnemyDetector.Instance.enemyList.Remove(gameObject);
        _moveSpeed = 0.0f;
        SwitchAnim();
        Invoke(nameof(PutObjectInPool), 1.0f);
        GetComponent<Collider2D>().isTrigger = true;
        // 从敌人探测器列表中移除该对象
    }

    public void GetDamaged(GameObject damageMaker)
    {
        // DamagePopupManager.Create(transform.position, (int)damageMaker.GetComponent<Skill>().damage);
        if (health - PlayerController.Instance.attackFinal > 0.0f)
        {
            health -= PlayerController.Instance.attackFinal;
            Debug.Log("Monster health = " + health);
        }
        else
        {
            PlayerController.Instance.IncreaseExperience();
            SetDead();
        }

    }

}
