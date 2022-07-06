using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventListener : MonoSingleton<EventListener>
{
    public int enemyKills = 0;
    public enum MessageEvent
    {
        Message_LevelUp,
        Message_SpawnBoss,
        Message_KillEnemy,
    }
    
    //定义一个字典，用来管理所有的事件
    public Dictionary<MessageEvent, Delegate> MessageList = new Dictionary<MessageEvent, Delegate>();

    private delegate void LevelUp();

    private delegate void EnemyKills();

    private LevelUp _levelUp;

    private EnemyKills _enemyKills;
    
    // Start is called before the first frame update
    void Start()
    {
        _levelUp = new LevelUp(UIManager.Instance.ShowBasicPropUI);
        _levelUp += PlayerController.Instance.LevelUp;
        _enemyKills = new EnemyKills(AddEnemyKills);
        
        AddListener(MessageEvent.Message_LevelUp, _levelUp);
        AddListener(MessageEvent.Message_KillEnemy, _enemyKills);
    }
    
    //消息触发
    public void SendMessage(MessageEvent msgID) {
        Delegate func;
        //去事件管理列表中找，有没有这个事件的订阅记录，有的话，就执行对应绑定的方法
        if (MessageList.TryGetValue(msgID, out func)) {
            if (func != null)
            {
                func.DynamicInvoke();
            }
        }
    }
    
    public void AddListener(MessageEvent msg_id, Delegate func) {
        MessageList[msg_id] = func;
    }

    public void AddEnemyKills()
    {
        ++enemyKills;
        Debug.Log("enemyKills is :" + enemyKills);
    }
}
