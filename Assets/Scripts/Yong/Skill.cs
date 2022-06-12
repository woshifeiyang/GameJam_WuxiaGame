using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{
    public float skillSpeed;

    public float damage;

    public bool isDisappearable;

    private Rigidbody2D _rb;

    private GameObject _enemy;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();

        if (_enemy)
        {
            _rb.AddForce((_enemy.transform.position - _player.transform.position).normalized * skillSpeed, ForceMode2D.Force);
        }
        Invoke("SelfDestory", 10.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
