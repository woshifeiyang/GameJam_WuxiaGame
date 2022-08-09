using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowKills : MonoBehaviour
{
    public Text killsText;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killsText.text = "Kills ="+Main.Instance.enemyKills.ToString();
    }
}
