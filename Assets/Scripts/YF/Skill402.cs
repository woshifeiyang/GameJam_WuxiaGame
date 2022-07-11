using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Skill402 : BulletSkillBase
{
    private Rigidbody2D _rb;
    public bool first = true;
    
    // Start is called before the first frame update
    public override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector3 v = EnemyDetector.Instance.GetNearestEnemyLoc()- PlayerController.Instance.transform.position;
        v.z = 0;
        float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        _rb.AddForce(v.normalized * speed, ForceMode2D.Force);

    }
    // Update is called once per frame
    public override void Update()
    {
        if(first == false)
        {
            if(Vector3.Distance(_rb.transform.position, PlayerController.Instance.GetPlayerPosition()) <= 1.0f)
            {
                SelfDestory();
                if (PlayerController.Instance.curHealth < PlayerController.Instance.GetPlayerHealthFinal())
                    PlayerController.Instance.curHealth += 1;
            }
            Vector3 playerPosition = PlayerController.Instance.GetPlayerPosition();
            Vector3 y = playerPosition - _rb.transform.position;
            y.z = 0;
            float angle = Vector3.SignedAngle(Vector3.up, y, Vector3.forward);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
            _rb.MovePosition(_rb.transform.position + (playerPosition - _rb.transform.position).normalized * speed * 0.1f * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy") && first == true)
        {
            first = false;
        }
    }


    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}