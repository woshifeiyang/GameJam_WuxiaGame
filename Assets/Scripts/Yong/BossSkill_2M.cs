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
