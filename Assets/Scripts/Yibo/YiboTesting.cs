using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YiboTesting : MonoBehaviour
{
    // Start is called before the first frame update
    public DamagePopupManager DPM;

    private void Awake()
    {
        DPM = GameObject.FindWithTag("DamagePopupManager").GetComponent<DamagePopupManager>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StatisticManager.addEnemyKilled(1);
    }
}
