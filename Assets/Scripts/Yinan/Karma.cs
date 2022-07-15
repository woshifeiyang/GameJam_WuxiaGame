using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karma : FieldSkillBase
{
    bool karma = false;
    float time = 0;
    int initialKillnumber;

    public void EnableKarma()
    {
        karma = true;
        int initialKillnumber = EventListener.Instance.enemyKills;
    }

    public override void Update()
    {
        time += Time.deltaTime;
        if(time >= 1f)
        {
            time = 0;
            if (karma)
            {
                if(EventListener.Instance.enemyKills - initialKillnumber > 10)
                {
                    IncreaseHP();
                }
            }
            
        }
    }

    void IncreaseHP()
    {
        PlayerController.Instance.maxHealth += 1f;
        initialKillnumber = EventListener.Instance.enemyKills;
    }

}
