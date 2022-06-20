using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{
    public float skillSpeed;

    public float damage;

    public bool isDisappearable;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _rb.AddForce((PlayerController.PlayerControllerInstance.GetNearestEnemyLoc() - PlayerController.PlayerControllerInstance.transform.position).normalized * skillSpeed, ForceMode2D.Force);
        
        Invoke("SelfDestory", 10.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
