using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Animator _anim;
    
    private CircleCollider2D _cc;
    
    private Vector2 _movement;

    private GameObject _nearestEnemy;

    private bool _hasFoundEnemy;

    private int _curExperience;

    private int _totalExperience;

    private int level;
    
    private MMProgressBar _mmProgressBar;

    public static PlayerController PlayerControllerInstance;

    public Transform skill;

    public float moveSpeed;

    public float skillCd;

    public float health;

    public float maxHealth;

    private Vector3 importedLocalScale;

    public FloatingJoystick floatingJoystick;
    private void Awake()
    {
        PlayerControllerInstance = this;
        importedLocalScale = this.transform.localScale;
        Debug.Log("X: " + importedLocalScale.x + "Y: " + importedLocalScale.y + "Z: " + importedLocalScale.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _cc = GetComponent<CircleCollider2D>();
        if (GameObject.Find("HorizontalBar") != null)
        {
            _mmProgressBar = GameObject.Find("HorizontalBar").GetComponent<MMProgressBar>();
            _mmProgressBar.UpdateBar01(health / maxHealth);
        }

        _hasFoundEnemy = false;
        _totalExperience = 30;
        InvokeRepeating("SpawnSkill", 1.0f, skillCd);
        StartCoroutine("FindNearestTarget");
        
    }

    // Update is called once per frame
    void Update()
    {
        //_movement.x = Input.GetAxisRaw("Horizontal");
        //_movement.y = Input.GetAxisRaw("Vertical");
        _movement = floatingJoystick.Direction;
        if (_movement.x != 0)
        {
            transform.localScale = new Vector3(-1.0f * _movement.x * importedLocalScale.x, importedLocalScale.y, importedLocalScale.z);
        }
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
        if (_nearestEnemy)
        {
            Debug.DrawLine(transform.position, _nearestEnemy.transform.position, Color.red);
        }
        
    }

    private void SwitchAnim()
    {
        _anim.SetFloat("speed", _movement.magnitude);
        if (health <= 0.0f)
        {
            _anim.SetBool("isDead", true);
        }
    }

    void SpawnSkill()
    {
        Instantiate(skill, new Vector3(transform.position.x, transform.position.y), transform.rotation);
    }

    IEnumerator FindNearestTarget()
    {
        float radius = _cc.radius;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (_hasFoundEnemy || _cc.radius >= 10.0f)
            {
                _cc.radius = radius;
                _hasFoundEnemy = false;
            }
            else _cc.radius += 0.5f;
        }
    }
    // 敌人探测器
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Monster>().isDead == false)
        {
            _nearestEnemy = other.gameObject;
            _hasFoundEnemy = true;
        }
    }
    // 被怪物攻击
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && col.gameObject.GetComponent<Monster>().isDead == false)
        {
            Debug.Log("main character was attacked by enemy");
            if(health - col.gameObject.GetComponent<Monster>().damage > 0.0f)
            {
                health -= col.gameObject.GetComponent<Monster>().damage;
                _mmProgressBar.UpdateBar01(health / maxHealth);
            }
            else
            {
                ExitGame();
            }
        }
    }
    
    public Vector3 GetNearestEnemyLoc()
    {
        if (_nearestEnemy && _nearestEnemy.activeInHierarchy)
        {
            return _nearestEnemy.transform.position;
        }
        return new Vector3(1.0f, 0.0f, 0.0f);
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void IncreaseExperience()
    {
        if (_curExperience + 1 == _totalExperience)
        {
            ++level;
            _curExperience = 0;
            _totalExperience += 5;
        }else if (_curExperience < _totalExperience)
        {
            ++_curExperience;
        }
    }
    
    private void ExitGame()
    {
        //预处理
    #if UNITY_EDITOR    //在编辑器模式下
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
