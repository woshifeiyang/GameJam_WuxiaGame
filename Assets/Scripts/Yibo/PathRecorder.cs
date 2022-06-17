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
    void Awake()
    {
        pathwayPoint = new Vector3[recordNum];
        for (int i = 0; i < recordNum; i++)
        {
            pathwayPoint[i] = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeRecord += Time.deltaTime;
        if (timeRecord > .5)
        {
            //drag position from the start of the array
            for (int i = recordNum - 1; i >= 0; i--)
            {
                if (i < recordNum - 1)
                {
                    pathwayPoint[i + 1] = pathwayPoint[i];
                }
            }
            // set t - 0 position to the current 0.5 second before position
            pathwayPoint[0] = new Vector3(transform.position.x, transform.position.y, 0);
            recordCursor++;
            timeRecord = 0f;

        }
        
    }
}
