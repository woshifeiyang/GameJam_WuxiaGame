using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpring : MonoBehaviour
{
    public float cd;

    private GameObject _recoverHpPoint;

    private ParticleSystem _particleSystem;

    private float _timer = 0;

    private bool _canRecover = true;
    // Start is called before the first frame update
    void Start()
    {
        string assertPath = "Prefab/ParticleSystem/SpringHPRecoverParticleSystem";
        _recoverHpPoint = (GameObject)Instantiate(Resources.Load(assertPath));
        _recoverHpPoint.transform.position = transform.position;
        _particleSystem = _recoverHpPoint.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= cd)
        {
            _canRecover = true;
            if(_particleSystem.isStopped) _particleSystem.Play();
            _timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && _canRecover)
        {
            PlayerController.Instance.RecoverHP();
            _particleSystem.Stop();
            _canRecover = false;
        }
    }
}
