using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] importMaps;
    public GameObject[,] maps;
    public GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
        maps = new GameObject[3, 3];
        int mapImportNum = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                maps[i,j] = importMaps[mapImportNum];
                mapImportNum++;
            }
        }
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y,0);

        for(int i = 0; i < importMaps.Length; i++)
        {
            importMaps[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
       
    }
}
