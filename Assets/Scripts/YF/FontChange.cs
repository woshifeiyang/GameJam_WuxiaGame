using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FontChange : MonoBehaviour
{

    public Font fontC;
    public Font fontE;
    public Text tt;
    public Text tt1;
    public Text tt2;
    public Text tt3;
    public Text tt4;
    public Text tt5;
    public Text tt6;
    public Text tt7;
    public Text tt8;



    // Use this for initialization

    public void Chinese()
    {
        tt.font=fontC;
        tt1.font = fontC;
        tt2.font = fontC;
        tt3.font = fontC;
        tt4.font = fontC;
        tt5.font = fontC;
        tt6.font = fontC;
        tt7.font = fontC;
        tt8.font = fontC;

    }

    public void English()
    {
        tt.font = fontE;
        tt1.font = fontE;
        tt2.font = fontE;
        tt3.font = fontE;
        tt4.font = fontE;
        tt5.font = fontE;
        tt6.font = fontE;
        tt7.font = fontE;
        tt8.font = fontE;
    }
}
