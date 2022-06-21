using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Animator _anim;
    
    private CircleCollider2D _cc;
    
    private Vector2 _movement;

    private GameObject _nearestEnemy;

    private bool _hasFoundEnemy;

    public static PlayerController PlayerControllerInstance;

    public Transform skill;

    public float moveSpeed;

    public float skillCd;

    public float health;

    private void Awake()
    {
        PlayerControllerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _cc = GetComponent<CircleCollider2D>();

        _hasFoundEnemy = false;
        InvokeRepeating("SpawnSkill", 1.0f, skillCd);
        StartCoroutine("FindNearestTarget");
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.x != 0)
        {
            transform.localScale = new Vector3(-1.0f * _movement.x * 0.4f, 0.4f, 1);
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
        if (health <= 0.0f)
        {
            ExitGame();
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
        Instantiate(skill, new Vector3(transform.position.x, transform.position.y + 1.0f), transform.rotation);
    }

    IEnumerator FindNearestTarget()
    {
        float radius = _cc.radius;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (_hasFoundEnemy || _cc.radius >= 40.0f)
            {
                _cc.radius = radius;
                _hasFoundEnemy = false;
            }
            else _cc.radius += 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Monster>().isDead == false)
        {
            _nearestEnemy = other.gameObject;
            _hasFoundEnemy = true;
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
    
    public void ExitGame()
    {
        //预处理
    #if UNITY_EDITOR    //在编辑器模式下
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
