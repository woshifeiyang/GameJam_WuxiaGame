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
        //Debug.Log("enter Awake");
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
            tempProduct.GetComponent<Monster>().poolBelongTo = "";
            tempProduct.GetComponent<Monster>().SetAlive();
            tempProduct.GetComponent<Monster>().health = 99999f;
            tempProduct.tag = "NotDamagableEnemy";
            StartCoroutine(tagChange(tempProduct));
        }
        transform.SetParent(null);
        StartCoroutine(SelfRecycle());
    }

    IEnumerator tagChange(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.9f);
        gameObject.tag = "Enemy";
        gameObject.GetComponent<Monster>().health = 100;
    }

    IEnumerator SelfRecycle()
    {
        yield return new WaitForSeconds(1f);
        if (enemyParent!=null)
        {
            transform.SetParent(enemyParent.transform);
        }
        else
        {
            Destroy(this);
        }

        gameObject.SetActive(false);
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
