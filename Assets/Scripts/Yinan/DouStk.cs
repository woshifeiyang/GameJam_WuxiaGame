using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DouStk : Strike
{
    public bool dou = false;
    // Start is called before the first frame update
   

    // Update is called once per frame
    public void EnableAnti()
    {
        dou = true;
    }
    
    public bool GetDou()
    {
        return dou;
    }
}
