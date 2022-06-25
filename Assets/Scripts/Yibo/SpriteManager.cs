using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public int spriteSlot = 2;
    public int playerSlot = 1;
    public int shareableSlot = 1;
    
    public Dictionary<string, Sprite> spriteLibrary = new Dictionary<string, Sprite>();
    // dictionary for subset sprites
    public Dictionary<string, float> spriteManagerProperty = new Dictionary<string, float>();

    void Start()
    {
        UpdateSprite();
        PlayerController pcr = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pcr.updateParameters();
    }
    public void AddNewSprite(string spriteName, Sprite targetSprite)
    {
        // add new sprite entity to the library
        // KEY is spriteName
        // VALUE is targetSprite
        if (!spriteLibrary.ContainsKey(spriteName))
        {
            spriteLibrary.Add(spriteName, targetSprite);
            Debug.Log("add new sprite failed, sprite with same name already exists.");
            foreach (var VARIABLE in spriteLibrary)
            {
                //Debug.Log(VARIABLE.Key);
            }
        }
        else
        {
            spriteLibrary[spriteName] = targetSprite;
        }
    }

    public void DeleteSprite(string spriteName)
    {
        //detele value with selected name
        if (spriteLibrary.ContainsKey(spriteName))
        {
            spriteLibrary.Remove(spriteName);
        }
        else
        {
            Debug.Log("delete failed, sprite name does not found.");
        }
    }

    public Sprite GetSprite(string spriteName)
    {
        return spriteLibrary[spriteName];
    }

    public void UpdateSprite()
    {
        foreach (KeyValuePair<string,Sprite> entry in spriteLibrary)
        {
            //check sprite Usage status, 0 -> sprite will be used
            if (entry.Value.spriteUsageStatus == 0 && entry.Value.spriteManagerCheckStatus)
            {
                Sprite tempSprite = entry.Value;
                foreach (KeyValuePair<string, float> spriteEntry in tempSprite.spriteProperty)
                {
                    //detect if the value is inserted
                    if (!spriteManagerProperty.ContainsKey(spriteEntry.Key))
                    {
                        spriteManagerProperty.Add(spriteEntry.Key, spriteEntry.Value);
                    }
                    else
                    {
                        //add the entry value with the spriteManagerProperty value together
                        float tempFloat = spriteManagerProperty[spriteEntry.Key];
                        tempFloat += spriteEntry.Value;
                        spriteManagerProperty[spriteEntry.Key] = tempFloat;
                    }
                    //Debug.Log(spriteEntry.Key + " : " + spriteManagerProperty[spriteEntry.Key]);
                }

                entry.Value.spriteManagerCheckStatus = false;
            }
        }
    }
    
    
    
    
}
