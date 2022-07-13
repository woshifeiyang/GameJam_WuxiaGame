using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_2M : ScopeSkillBase
{
    private Collider[] _colliders;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        // 持续时间结束时销毁自身
        Invoke("SelfDestory", skillTime);
        _colliders = Physics.OverlapSphere(PlayerController.Instance.GetPlayerPosition(), range);
        if (_colliders.Length == 0)
        {
            Debug.Log("There is no collider in the sphere");
            SelfDestory();
        }

        for (int i = 0; i < _colliders.Length; i++)
        {
            if (_colliders[i].gameObject.CompareTag("Player"))
            {
                _player = _colliders[i].gameObject;
                break;
            }
        }
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
