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

    public Vector3 importedLocalScale;
    void Start()
    {
        importedLocalScale = transform.localScale;
        
        Player = GameObject.FindGameObjectWithTag("Player");
        PR = Player.GetComponent<PathRecorder>();
        Offset = PR.pathwayPoint[targetPointSecond] - transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PR.pathwayPoint[targetPointSecond] - Offset, Speed * Time.deltaTime);
    }
    
    private void FixedUpdate()
    {
        if (transform.position.x < (PR.pathwayPoint[targetPointSecond] - Offset).x)
        {
            transform.localScale = new Vector3(-1f * importedLocalScale.x, importedLocalScale.y, importedLocalScale.y);
        }
        else
            transform.localScale = importedLocalScale;
    }
}
