using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class Sprite :  MonoBehaviour
{
    public string spriteName;
    public bool spriteManagerCheckStatus = true;
    // false = checked
    // true = need to be checked
    public int spriteUsageStatus = 0;
    // sprite status spreadsheet
    // 0 = normal using
    // 1 = not using
    public float quality;

    public float damageFactor;
    public float projectileSize;
    public float projectileNum;
    public float coolDownReduce;
    public float healthBonus;
    public float damageReduction;
    public float movingSpeedBonus;
    public float currencyBonusFactor;
    public float shieldNumAdd;
    public float shieldCooldownReduction;
    public float respawnChanceBonus;
    public float luckinessFactor;
    public float killCountFactor;
    public float bossDamageFactor;
    public float healthStealthFactor;
    public float itemBonus;
    
    //define sprite dictionary
    public Dictionary<string, float> spriteProperty = new Dictionary<string, float>();

    private void Awake()
    {
        AddNums();
        AddNewSprite();
    }

    public void AddNewSprite()
    {
        SpriteManager spriteManager = GameObject.FindObjectOfType<SpriteManager>();
        spriteManager.AddNewSprite(spriteName, this);
        spriteManager.UpdateSprite();
    }

    private void AddNums()
    {
        spriteProperty.Add("quality",quality);
        
        spriteProperty.Add("damageFactor", damageFactor);
        spriteProperty.Add("projectileSize", projectileSize);
        spriteProperty.Add("projectileNum", projectileNum);
        spriteProperty.Add("coolDownReduce", coolDownReduce);
        spriteProperty.Add("healthBonus", healthBonus);
        spriteProperty.Add("damageReduction", damageReduction);
        spriteProperty.Add("movingSpeedBonus", movingSpeedBonus);
        spriteProperty.Add("currencyBonusFactor", currencyBonusFactor);
        spriteProperty.Add("shieldNumAdd", shieldNumAdd);
        spriteProperty.Add("shieldCooldownReduction", shieldCooldownReduction);
        spriteProperty.Add("respawnChanceBonus", respawnChanceBonus);
        spriteProperty.Add("luckinessFactor", luckinessFactor);
        spriteProperty.Add("killCountFactor", killCountFactor);
        spriteProperty.Add("bossDamageFactor", bossDamageFactor);
        spriteProperty.Add("healthStealthFactor", healthStealthFactor);
        spriteProperty.Add("itemBonus", itemBonus);
    }

    public void StartUsingSprite()
    {
        spriteUsageStatus = 0;
    }
}
