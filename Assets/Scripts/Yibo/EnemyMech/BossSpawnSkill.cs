using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSpawnSkill : MonoBehaviour
{
    public int[] spawnPhases = {20, 60, 1000};

    public GameObject[] enemiesToSpawn;

    private Transform[] spawnCollidersTransforms;

    private BoxCollider2D[] spawnColliders;

    private int phaseCursor = 0;

    private float tempTime = 0f;

    void Start()
    {
        updateCollider();
        StartCoroutine(BossSpawnAction(enemiesToSpawn[phaseCursor]));
    }

    private void FixedUpdate()
    {
        tempTime += Time.deltaTime;

        if (tempTime >= spawnPhases[phaseCursor])
        {
            phaseCursor++;
            StartCoroutine(BossSpawnAction(enemiesToSpawn[phaseCursor]));
        }
    }

    BoxCollider2D GetRandomCollider()
    {
        BoxCollider2D result;
        
        float tempSize = (float)spawnColliders.Length - 1;
        int colliderIndex = Mathf.FloorToInt(Random.Range(0f, tempSize - 0.01f));
        result = spawnColliders[colliderIndex];

        return result;
    }
    
    
    public void updateCollider()
    {
        spawnColliders = new BoxCollider2D[transform.childCount];
        spawnCollidersTransforms = new Transform[transform.childCount];
        //Debug.Log("childlength" + locationHolder.childCount);
        for (int i = 0; i <  spawnCollidersTransforms.Length; i++)
        {
            spawnCollidersTransforms[i] = transform.GetChild(i);
            spawnCollidersTransforms[i].position = new Vector3( spawnCollidersTransforms[i].position.x,  spawnCollidersTransforms[i].position.y, transform.position.z);
            spawnColliders[i] = spawnCollidersTransforms[i].GetComponent<BoxCollider2D>();
        }
    }
    
    IEnumerator BossSpawnAction(GameObject spawnTarget)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector3 targetPosition = GetRandomPointInsideCollider(GetRandomCollider());
            GameObject spawnedTarget = Instantiate(spawnTarget, targetPosition, Quaternion.identity);
        }
    }
    
    private Vector3 GetRandomPointInsideCollider( BoxCollider2D boxCollider )
    {
        Vector2 extents = boxCollider.size / 2f;
        Vector2 point = new Vector2(
            Random.Range( -extents.x, extents.x ),
            Random.Range( -extents.y, extents.y )
        )  + boxCollider.offset;
        return boxCollider.transform.TransformPoint( point );
    }
}
