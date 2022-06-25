using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
public class HeroHP : MonoBehaviour
{
    public const int MAX_HP = 100;
    public int initialHP = 80;
    public int currentHP;
    private MMProgressBar mmProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = initialHP;
        mmProgressBar = GameObject.Find("HPBar").GetComponent<MMProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
            float percentHP = (float)currentHP / MAX_HP;
            if (Input.GetKeyDown("space"))
            {
                currentHP += (int)Random.Range(-10.0f, 10.0f);
                if (currentHP > MAX_HP)
                    currentHP = MAX_HP;
                if (currentHP < 0)
                    currentHP = 0;
            }
            mmProgressBar.UpdateBar01(percentHP);

        }
    
}