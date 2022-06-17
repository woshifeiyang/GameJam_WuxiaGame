using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public GameObject Player;
    public int targetPointSecond = 0;
    public int Speed = 10;
    
    private Vector3 Offset;
    
    private PathRecorder PR;

    void Start()
    {
        PR = Player.GetComponent<PathRecorder>();
        Offset = PR.pathwayPoint[targetPointSecond] - transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PR.pathwayPoint[targetPointSecond] - Offset, Speed * Time.deltaTime);
    }
}
