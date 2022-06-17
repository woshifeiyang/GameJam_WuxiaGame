using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRecorder : MonoBehaviour
{
    public int recordNum = 5;
    
    public Vector3[] pathwayPoint;
    // Start is called before the first frame update

    private float timeRecord = 0f;
    private int recordCursor = 0;
    void Start()
    {
        for (int i = 0; i < recordNum; i++)
        {
            pathwayPoint[i] = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeRecord += Time.deltaTime;
        if (timeRecord > 1)
        {
            pathwayPoint[recordCursor] = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
