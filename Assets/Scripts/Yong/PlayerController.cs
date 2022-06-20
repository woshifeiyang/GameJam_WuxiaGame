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

    private bool _hasFound;

    public Transform skill;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _cc = GetComponent<CircleCollider2D>();

        _hasFound = false;
        InvokeRepeating("SpawnSkill", 1.0f, 3.0f);
        StartCoroutine("FindNearestTarget");
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.x != 0)
        {
            transform.localScale = new Vector3(_movement.x, 1.0f, 1);
        }
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * speed * Time.fixedDeltaTime);
        if (_nearestEnemy)
        {
            Debug.DrawLine(transform.position, _nearestEnemy.transform.position, Color.red);
        }
    }

    private void SwitchAnim()
    {
        _anim.SetFloat("speed", _movement.magnitude);
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
            if (_hasFound || _cc.radius >= 40.0f)
            {
                _cc.radius = radius;
                _hasFound = false;
            }
            else _cc.radius += 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _nearestEnemy = other.gameObject;
            _hasFound = true;
        }
    }
}
