using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoSingleton<Main>
{
    public int enemyKills = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.InitPlayerController();
        JsonManager.Instance.InitJsonManager();
        UIManager.Instance.InitUIManager();
        EventListener.Instance.InitEventListener();
        
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_GetSkill);
        }
    }
    // 管理游戏整体进程逻辑
    private void GameStart()
    {
        EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_FirstChoose);
    }
    
    public void AddEnemyKills()
    {
        ++enemyKills;
    }
}
