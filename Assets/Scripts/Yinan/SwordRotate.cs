using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotate : MonoBehaviour
{
    private float rotateSpeed = 400;

    public Transform center;
    private float distance;
    private Vector3 dir;
    void Start()
    { 
        center = GameObject.Find("Player").transform;
        distance = Vector3.Distance(transform.position,center.position);
        dir = transform.position - center.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = center.position + dir.normalized * distance;
        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        dir = transform.position - center.position;
    }
}
