using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_1M : BulletSkillBase
{
    // Start is called before the first frame update
    void Start()
    {
        // 持续时间结束时销毁自身
        Invoke("SelfDestory", skillTime);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
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
            SelfDestory();
        }
    }
    
    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
