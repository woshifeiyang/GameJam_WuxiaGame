using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] importMaps;
    //maps importer
    public GameObject[,] maps;
    public GameObject player;
    public int mapSize = 100;
    //mapsize in unity unit
    public int mapLength = 3;
    //number of maps in one direction, 3 means 3x3 maps


    private Vector3 playerPos;
    private int[] currentPosNum;
    private int currentQuadrant;
    private int[] previousPosNum;
    // Start is called before the first frame update
    void Start()
    {

        //get maps into 2d array
        maps = new GameObject[mapLength, mapLength];
        int mapImportNum = 0;
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapLength; j++)
            {
                maps[i, j] = importMaps[mapImportNum];
                mapImportNum++;
            }
        }
        //get player pos
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        for (int i = 0; i < importMaps.Length; i++)
        {
            //importMaps[i].SetActive(false);
            //Set all maps to deactive
        }

        currentPosNum = this.getLocatingMapNum();
        previousPosNum = currentPosNum;
        //inject pos data
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //update player pos and location
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        currentPosNum = this.getLocatingMapNum();
        currentQuadrant = this.getQuadrant();

        //this.triggerMap();
        //trigger adjacent maps

        if(currentPosNum[0] != previousPosNum[0] || currentPosNum[1] != previousPosNum[1]) 
        {
            this.moveMap();
            //Debug.Log("c" + currentPosNum[0] + " " + currentPosNum[1]);
            //Debug.Log("p" + previousPosNum[0] + " " + previousPosNum[1]);
            //Debug.Log("change");
            previousPosNum = currentPosNum;
            
        }

        //debug
        //Debug.Log(currentPosNum[0] + " " + currentPosNum[1]);
        //Debug.Log(currentQuadrant);
    }

    private int[] getLocatingMapNum()
    {
        int[] locatingMapNum = { 1, 1 };
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapLength; j++)
            {
               
                Vector3 mapPos = maps[i, j].transform.position;
                if ((Mathf.Abs(mapPos.x - playerPos.x) < mapSize / 2) && (Mathf.Abs(mapPos.y - playerPos.y) < mapSize / 2))
                {
                    //calculate which map the player is in the range of
                    locatingMapNum = new int[] { i, j };
                    return locatingMapNum;
                }
            }
        }
        return previousPosNum;
    }

    private int getQuadrant()
    {
        //  1|2
        //  3|4
        int quadrantNum = 1;
        GameObject tempMap = maps[currentPosNum[0], currentPosNum[1]];
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
        int left = currentPosNum[1] - 1, right = currentPosNum[1] + 1, up = currentPosNum[0] + 1, down = currentPosNum[0] - 1;
        if (left == -1)
            left = 2;
        if (right == 3)
            right = 0;
        if (up == 3)
            up = 0;
        if (down == -1)
            down = 2;
        if (currentQuadrant == 1)
        {
            maps[up, left].SetActive(true);
            maps[currentPosNum[0], left].SetActive(true);
            maps[up, currentPosNum[1]].SetActive(true);
        }
        else if (currentQuadrant == 2)
        {
            maps[up, right].SetActive(true);
            maps[currentPosNum[0], right].SetActive(true);
            maps[up, currentPosNum[1]].SetActive(true);
        }
        else if (currentQuadrant == 3)
        {
            maps[down, left].SetActive(true);
            maps[currentPosNum[0], left].SetActive(true);
            maps[down, currentPosNum[1]].SetActive(true);
        }
        else
        {
            maps[down, right].SetActive(true);
            maps[currentPosNum[0], right].SetActive(true);
            maps[down, currentPosNum[1]].SetActive(true);
        }
    }

    private void moveMap() 
    {
        GameObject tempCMap = maps[currentPosNum[0], currentPosNum[1]];
        GameObject tempPMap = maps[previousPosNum[0], previousPosNum[1]];
        // get map objects
        // cmap = current map, p map = previous map
        if (tempCMap.transform.position.y > tempPMap.transform.position.y) 
        {
            //Debug.Log("went up");
            //went up
            float tempPos = maps[0, 0].transform.position.y;
            int targetMapNum = 0;
            for (int i = 0; i < mapLength; i++)
            {
                if(maps[i, 0].transform.position.y < tempPos)
                {
                    tempPos = maps[i, 0].transform.position.y;
                    targetMapNum = i;
                }
            }      
                for (int i = 0; i < mapLength; i++) 
                {
                    maps[targetMapNum, i].transform.position = new Vector3(         maps[targetMapNum, i].transform.position.x, 
                                                                                    maps[targetMapNum, i].transform.position.y + (mapLength * mapSize), 
                                                                                    maps[targetMapNum, i].transform.position.z);
                    //maps[i, currentPosNum[1] - 1].transform.position.y += 2 * mapSize;
                }
        }    
        else if (tempCMap.transform.position.y < tempPMap.transform.position.y)
        {

            float tempPos = maps[0, 0].transform.position.y;
            int targetMapNum = 0;
            for (int i = 0; i < mapLength; i++)
            {
                if (maps[i, 0].transform.position.y > tempPos)
                {
                    tempPos = maps[i, 0].transform.position.y;
                    targetMapNum = i;
                }
            }
            //Debug.Log("went down");
                for (int i = 0; i < mapLength; i++)
                {
                    maps[targetMapNum, i].transform.position = new Vector3(         maps[targetMapNum, i].transform.position.x, 
                                                                                    maps[targetMapNum, i].transform.position.y - (mapLength * mapSize), 
                                                                                    maps[targetMapNum, i].transform.position.z);
                    //maps[i, currentPosNum[1] + 1].transform.position.y -= 2 * mapSize;
                }
                //went down
        }
       

        if (tempCMap.transform.position.x > tempPMap.transform.position.x)
        {

            float tempPos = maps[0, 0].transform.position.x;
            int targetMapNum = 0;
            for (int i = 0; i < mapLength; i++)
            {
                if (maps[0, i].transform.position.x < tempPos)
                {
                    tempPos = maps[0, i].transform.position.x;
                    targetMapNum = i;
                }
            }
            //Debug.Log("went right");
                for (int i = 0; i < mapLength; i++)
                {
                    maps[i, targetMapNum].transform.position = new Vector3( maps[i, targetMapNum].transform.position.x + (mapLength * mapSize), 
                                                                                    maps[i, targetMapNum].transform.position.y, 
                                                                                    maps[i, targetMapNum].transform.position.z);
                    //maps [currentPosNum[1] - 1 ,i].transform.position.x += 2 * mapSize;
                }
                //went right
        }
        else if (tempCMap.transform.position.x < tempPMap.transform.position.x)
        {
            float tempPos = maps[0, 0].transform.position.x;
            int targetMapNum = 0;
            for (int i = 0; i < mapLength; i++)
            {
                if (maps[0, i].transform.position.x > tempPos)
                {
                    tempPos = maps[0, i].transform.position.x;
                    targetMapNum = i;
                }
            }
            //Debug.Log("went left");
                for (int i = 0; i < mapLength; i++)
                {
                    maps[i, targetMapNum].transform.position = new Vector3( maps[i, targetMapNum].transform.position.x - (mapLength * mapSize), 
                                                                                    maps[i, targetMapNum].transform.position.y, 
                                                                                    maps[i, targetMapNum].transform.position.z);
                    //maps[currentPosNum[1] + 1, i].transform.position.x -= 2 * mapSize;
                }
                //went left
        }
        
        
    }

}
