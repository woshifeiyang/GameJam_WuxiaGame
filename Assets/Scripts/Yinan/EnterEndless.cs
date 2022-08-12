using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEndless : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Monster>().isDead)
        {
            EnemySpawner.Instance.Timer = 1795f;
        }
    }
}
