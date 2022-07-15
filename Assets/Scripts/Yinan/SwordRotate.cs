using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotate : MonoBehaviour
{
    private float rotateSpeed = 150;

    public Transform center;
    void Start()
    {
        center = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
