using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    //things need to be tracked

    // enemy killed of a player for the whole time
    public static int enemyKilledTotal;
    // current level enemy count
    public static int currentLevelEnemyKilled;

    // projectile launched of a player for the whole time
    public static int projectileCountTotal;


    public static void addEnemyKilled(int num) 
    {
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
}
