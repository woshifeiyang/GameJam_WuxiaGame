using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoSingleton<EventListener>
{
    
    public enum MessageEvent
    {
        Message_FirstChoose,
        Message_BasicPropLevelUp,
        Message_GetSkill,
        Message_SpawnBoss,
        Message_GameOver,
    }
    
    //定义一个字典，用来管理所有的事件
    public Dictionary<MessageEvent, Delegate> MessageList = new Dictionary<MessageEvent, Delegate>();

    private delegate void FirstChoose();
    
    private delegate void BasicPropLevelUp();

    private delegate void GetSkill();

    private delegate void GameOver();

    private FirstChoose _firstChoose;

    private BasicPropLevelUp _basicPropLevelUp;

    private GetSkill _getSkill;

    private GameOver _gameOver;

    public void InitEventListener()
    {
        _firstChoose = new FirstChoose(UIManager.Instance.ShowFirstChooseUI);
        
        _basicPropLevelUp = new BasicPropLevelUp(UIManager.Instance.ShowBasicPropUI);
        _basicPropLevelUp += PlayerController.Instance.LevelUp;

        _getSkill = new GetSkill(UIManager.Instance.ShowSkillListUI);

        _gameOver = new GameOver(UIManager.Instance.ShowEvaluationUI);
        
        AddListener(MessageEvent.Message_FirstChoose, _firstChoose);
        AddListener(MessageEvent.Message_BasicPropLevelUp, _basicPropLevelUp);
        AddListener(MessageEvent.Message_GetSkill, _getSkill);
        AddListener(MessageEvent.Message_GameOver, _gameOver);
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
    
    private void AddListener(MessageEvent msg_id, Delegate func) {
        MessageList[msg_id] = func;
    }
    
}
