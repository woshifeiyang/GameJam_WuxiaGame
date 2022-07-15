using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_2M : ScopeSkillBase
{
    private Collider[] _colliders;

    private bool _hasFound;
    // Start is called before the first frame update
    void Start()
    {
        _hasFound = false;
        // 持续时间结束时销毁自身
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
    
    /*public void MakeDamageOnPlayer()
    {
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
                _hasFound = true;
                break;
            }
        }

        if (_hasFound)
        {
            if (PlayerController.Instance.curHealth - damage > 0.0f)
            {
                PlayerController.Instance.curHealth -= damage;
                PlayerController.Instance.PlayerDamageFeedback?.PlayFeedbacks();
            }
            else
            {
                EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_GameOver);
            }
        }
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (PlayerController.Instance.curHealth - damage > 0.0f)
            {
                PlayerController.Instance.curHealth -= damage;
                PlayerController.Instance.PlayerDamageFeedback?.PlayFeedbacks();
            }
            else
            {
                EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_GameOver);
            }
        }
    }
}
