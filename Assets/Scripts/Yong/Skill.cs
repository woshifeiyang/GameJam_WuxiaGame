using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{
    public float skillSpeed;

    private Rigidbody2D _rb;

    private GameObject _enemy;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce((_enemy.transform.position - _player.transform.position).normalized * skillSpeed, ForceMode2D.Force);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
