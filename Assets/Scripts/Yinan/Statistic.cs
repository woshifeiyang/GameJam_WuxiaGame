using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Statistic : MonoSingleton<Statistic>
{
    private float totalDamage;
    private float avgDamage;
    private int Kills;
    private int HitNumber;
    private int numberPerHit;
    //Timer
    private float timer;
    private float timerSkill;
    //Totaldamage
    private float td101;
    private float td102;
    private float td103;
    private float td104;
    private float td201;
    private float td202;
    private float td301;
    private float td303;
    private float td401;
    private float td402;

    
    //DPS
    private float dps101;
    private float dps102;
    private float dps103;
    private float dps104;
    private float dps201;
    private float dps202;
    private float dps301;
    private float dps303;
    private float dps401;
    private float dps402;

    //短时间dps
    private float sdps201;
    private float sdps202;
    float std;
    float stimer;
    //DPH
    private float dph201;
    private float dph202;

    //Timer
    private float timer101;
    private float timer102;
    private float timer103;
    private float timer104;
    private float timer201;
    private float timer202;
    private float timer301;
    private float timer303;
    private float timer401;
    private float timer402;
    

    // Start is called before the first frame update

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            //showDPS();
            timer = 0;
        }
        timerSkill += Time.deltaTime;
    }

    public void Dps101(float damage)
    {
        if (timer101 == 0)
        {
            timer101 = timerSkill;
        }
        float dpstimer = timerSkill - timer101;
        td101 += damage;
        dps101 = td101 / dpstimer;
    }
    public void Dps102(float damage)
    {
        if (timer102 == 0)
        {
            timer102 = timerSkill;
        }
        float dpstimer = timerSkill - timer102;
        td102 += damage;
        dps102 = td102 / dpstimer;
    }
    public void Dps103(float damage)
    {
        if (timer103 == 0)
        {
            timer103 = timerSkill;
        }
        float dpstimer = timerSkill - timer103;
        td103 += damage;
        dps103 = td103 / dpstimer;
    }
    public void Dps104(float damage)
    {
        if (timer104 == 0)
        {
            timer104 = timerSkill;
        }
        float dpstimer = timerSkill - timer104;
        td104 += damage;
        dps104 = td104 / dpstimer;
    }
    public void Dps201(float damage)
    {
        if(timer201 == 0)
        {
            timer201 = timerSkill;
        }        
        float dpstimer = timerSkill - timer201;
        td201 += damage;
        dps201 = td201 / dpstimer;        
        
        //应当以学习了新技能或者升级为分割点
        if (dpstimer-stimer>10)
        {
            std = td201;
            stimer = dpstimer;
        }
        sdps201 = (td201 - std) / (dpstimer - stimer);
    }
    public void Dps202(float damage)
    {
        if (timer202 == 0)
        {
            timer202 = timerSkill;
        }
        float dpstimer = timerSkill - timer202;
        td202 += damage;
        dps202 = td202 / dpstimer;
    }

    public void Dps301(float damage)
    {
        if (timer301 == 0)
        {
            timer301 = timerSkill;
        }
        float dpstimer = timerSkill - timer301;
        td301 += damage;
        dps301 = td301 / dpstimer;
    }
    public void Dps303(float damage)
    {
        if (timer303 == 0)
        {
            timer303 = timerSkill;
        }
        float dpstimer = timerSkill - timer303;
        td303 += damage;
        dps303 = td303 / dpstimer;
    }
    public void Dps401(float damage)
    {
        if (timer401 == 0)
        {
            timer401 = timerSkill;
        }
        float dpstimer = timerSkill - timer401;
        td401 += damage;
        dps401 = td401 / dpstimer;
    }
    public void Dps402(float damage)
    {
        if (timer402 == 0)
        {
            timer402 = timerSkill;
        }
        float dpstimer = timerSkill - timer402;
        td402 += damage;
        dps402 = td402 / dpstimer;
    }


    public void TotalDamage(float damage)
    {
        totalDamage += damage;
    }

    public void EnemyKill()
    {
        Kills++;
    }

    public void EnemyHit()
    {
        HitNumber++;
    }
    public void NumberPerHit()
    {
        numberPerHit++;
        Invoke("OutputNumberPerhit", 1.5f);
    }

    void OutputNumberPerhit()
    {
        if (numberPerHit != 0)
        {
            StreamWriter sw = File.AppendText("C:\\Users\\acer\\Desktop\\statistic.txt");
            Debug.Log("hitnumber =" + numberPerHit);
            sw.WriteLine(numberPerHit);
            numberPerHit = 0;
            sw.Close();
        }
       
    }
    
    void showDPS()
    {
        transform.Find("101").GetComponent<Text>().text = "101:" + Mathf.Ceil(dps101).ToString();
        transform.Find("102").GetComponent<Text>().text = "102:" + Mathf.Ceil(dps102).ToString();
        transform.Find("103").GetComponent<Text>().text = "103:" + Mathf.Ceil(dps103).ToString();
        transform.Find("104").GetComponent<Text>().text = "104:" + Mathf.Ceil(dps104).ToString();
        transform.Find("201").GetComponent<Text>().text = "201:" + Mathf.Ceil(dps201).ToString();
        transform.Find("202").GetComponent<Text>().text = "202:" + Mathf.Ceil(dps202).ToString();
        transform.Find("301").GetComponent<Text>().text = "301:" + Mathf.Ceil(dps301).ToString();
        transform.Find("303").GetComponent<Text>().text = "303:" + Mathf.Ceil(dps303).ToString();
        transform.Find("401").GetComponent<Text>().text = "401:" + Mathf.Ceil(dps401).ToString();
        transform.Find("402").GetComponent<Text>().text = "402:" + Mathf.Ceil(dps402).ToString();
    }
}

