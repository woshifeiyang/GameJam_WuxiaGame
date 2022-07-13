using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : Singleton<StatisticManager>
{
    //things need to be tracked

    // enemy killed of a player for the whole time
    public static int enemyKilledTotal = 50;
    // current level enemy count
    public static int currentLevelEnemyKilled;

    // projectile launched of a player for the whole time
    public static int projectileCountTotal;


    public static void addEnemyKilled(int num, int levelType) 
    {
        // num is the input enemykilled
        // level type specs, 0 = normal level, 1 = endless less
        if(levelType == 1)
        enemyKilledTotal += num;

        currentLevelEnemyKilled += num;
    }

    public static void clearEnemyKilledCount()
    {
        currentLevelEnemyKilled = 0;
    }

    public static void addProjectileCount(int num)
    {
        projectileCountTotal += num;
    }

    public static int getEnemyKilledTotal()
    {
        return enemyKilledTotal;
    }
}
