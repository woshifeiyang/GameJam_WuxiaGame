using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] importMaps;
    public GameObject[,] maps;
    public GameObject player;
    public int mapSize = 100;


    private Vector3 playerPos;
    private int[] currentPosNum;
    private int currentQuadrant;
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
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y,0);

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
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        currentPosNum = this.getLocatingMapNum();
        currentQuadrant = this.getQuadrant();

            //this.triggerMap();
        
        Debug.Log(currentPosNum[0] + " " + currentPosNum[1]);
        Debug.Log(currentQuadrant);
    }

    private int[] getLocatingMapNum() 
    {
        int[] locatingMapNum  = {1,1};
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector3 mapPos = maps[i, j].transform.position;
                if ((Mathf.Abs(mapPos.x - playerPos.x) < mapSize/2) && (Mathf.Abs(mapPos.y - playerPos.y) < mapSize / 2))
                {
                    locatingMapNum = new int[]{i,j};
                }
            }
        }
        return locatingMapNum;
    }

    private int getQuadrant() 
    {
        //  1|2
        //  3|4
        int quadrantNum = 1;
        GameObject tempMap = maps[currentPosNum[0],currentPosNum[1]];
        if ((tempMap.transform.position.y - playerPos.y) < 0)
        {
            // if the player pos is on the upper quadrant
            if ((tempMap.transform.position.x - playerPos.x) > 0)
            {
                quadrantNum = 1;
            }
            else 
            {
                quadrantNum = 2;
            }

        }
        else 
        {
            //player pos is on the lower quadrant
            if ((tempMap.transform.position.x - playerPos.x) > 0)
            {
                quadrantNum = 3;
            }
            else 
            {
                quadrantNum = 4;
            }
        }
        return quadrantNum;
    }

    private void triggerMap() 
    {
        GameObject tempMap = maps[currentPosNum[0], currentPosNum[1]];
        tempMap.SetActive(true);
        if (currentQuadrant == 1) 
        {
            maps[currentPosNum[0] + 1, currentPosNum[1] - 1].SetActive(true);
            maps[currentPosNum[0], currentPosNum[1] - 1].SetActive(true);
            maps[currentPosNum[0] + 1, currentPosNum[1]].SetActive(true);
        }
        else if (currentQuadrant == 2) 
        {
            maps[currentPosNum[0] + 1, currentPosNum[1] + 1].SetActive(true);
            maps[currentPosNum[0], currentPosNum[1] + 1].SetActive(true);
            maps[currentPosNum[0] + 1, currentPosNum[1]].SetActive(true);
        }
        else if (currentQuadrant == 3) 
        {
            maps[currentPosNum[0] - 1, currentPosNum[1] - 1].SetActive(true);
            maps[currentPosNum[0], currentPosNum[1] - 1].SetActive(true);
            maps[currentPosNum[0] - 1, currentPosNum[1]].SetActive(true);
        }
        else 
        {
            maps[currentPosNum[0] - 1, currentPosNum[1] + 1].SetActive(true);
            maps[currentPosNum[0], currentPosNum[1] + 1].SetActive(true);
            maps[currentPosNum[0] - 1, currentPosNum[1]].SetActive(true);
        }
    }
}
