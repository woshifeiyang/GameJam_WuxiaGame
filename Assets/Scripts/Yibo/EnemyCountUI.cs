using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountUI : MonoBehaviour
{
    public Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        text.text = StatisticManager.getEnemyKilledTotal().ToString();
    }
}
