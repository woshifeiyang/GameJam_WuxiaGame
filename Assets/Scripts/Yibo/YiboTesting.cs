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
        DPM.Create(Vector3.zero, 100);
        DPM.Create(Vector3.zero, 200);
        DPM.Create(Vector3.zero, 300);
        DPM.Create(Vector3.zero, 400);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
