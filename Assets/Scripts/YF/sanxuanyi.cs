using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanxuanyi : MonoBehaviour
{
    public string text;
    public int ID;
    public Text tt;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick()
    {
        tt.text = text;
    }
}
