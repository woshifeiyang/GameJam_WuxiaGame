using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public Dictionary<string, Sprite> spriteLibrary = new Dictionary<string, Sprite>();
    
    public void AddNewSprite(string spriteName, Sprite targetSprite)
    {
        // add new sprite entity to the library
        // KEY is spriteName
        // VALUE is targetSprite
        if (!spriteLibrary.ContainsKey(spriteName))
        {
            spriteLibrary.Add(spriteName, targetSprite);
        }
        else
        {
            spriteLibrary[spriteName] = targetSprite;
        }
    }

    public void DeleteSprite(string spriteName)
    {
        //detele value with selected name
        if(spriteLibrary.ContainsKey(spriteName))
        spriteLibrary.Remove(spriteName);
    }

    public Sprite GetSprite(string spriteName)
    {
        return spriteLibrary[spriteName];
    }
    
    
}
