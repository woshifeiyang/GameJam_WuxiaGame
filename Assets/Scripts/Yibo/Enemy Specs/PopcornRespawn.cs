using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornRespawn : MonoBehaviour
{
    public Transform locationHolder;

    public GameObject enemyParent;

    public GameObject deathProduct;

    private Monster monsterComponent;

    private Transform[] locations;
    void Start()
    {
        Debug.Log("enter Awake");
        monsterComponent = enemyParent.GetComponent<Monster>();

        updateLocatingPoint();
    }

    public void updateLocatingPoint()
    {
        
        locations = new Transform[locationHolder.childCount];
        //Debug.Log("childlength" + locationHolder.childCount);
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i] = locationHolder.GetChild(i);
            locations[i].position = new Vector3(locations[i].position.x, locations[i].position.y, transform.position.z);
        }
    }

    public void DeathMechanics()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            GameObject tempProduct = Instantiate(deathProduct, locations[i]);
            tempProduct.transform.SetParent(null);
        }
    }

    private void OnEnable()
    {
        updateLocatingPoint();
        DeathMechanics();
    }

    private void OnDrawGizmos()
    {
        updateLocatingPoint();
        //Debug.Log(locations.Length);
        Gizmos.color = Color.green;
        foreach (Transform t in locations)
        {
            Gizmos.DrawSphere(t.position, .1f);
        }
    }
}
