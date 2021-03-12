using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class ChopChopAnalytics : MonoBehaviour
{
    private int enemydamaged = 0;
    private int failedToBlock = 0;
    private int attackBlocked = 0;
    private int enemiesKilledSword = 0;
    private int enemiesKilledAxe = 0;
    private int enemiesKilledMace = 0;
    private int leftAttack = 0;
    private int rightAttack = 0;
    private int upAttack = 0;
    private string currentTime;


    public void IncrementEnemyDamaged()
    {
        enemydamaged++;
    }
    public void IncrementBlockFailed()
    {
        failedToBlock++;
    }
    public void IncrementBlockSuccess()
    {
        attackBlocked++;
    }
    
    public void TimeTrack()
    {
        currentTime = Time.time.ToString("f6");
        Debug.Log("Time elapsed is: " + currentTime);
        
        AnalyticsResult result = Analytics.CustomEvent("AvgGameSession" , new Dictionary<string, object>{
            {"mTimeTaken", currentTime},
            {"EnemyDamaged", enemydamaged},
            {"BlockFails", failedToBlock},
            {"BlockSuccess", attackBlocked},
            {"LeftAttack", leftAttack},
            {"RightAttack", rightAttack},
            {"UpAttack", upAttack},
            {"EnemiesKilledBySword", enemiesKilledSword },
            {"EnemiesKilledByAxe", enemiesKilledAxe },
            {"EnemiesKilledByMace", enemiesKilledMace }
        });
        Debug.Log("Result: " + result);

    }
    public void IncrementLeftAttack() 
    {
        leftAttack++;
    }
    public void IncrementRightAttack()
    {
        rightAttack++;
    }
    public void IncrementUpAttack() 
    {
        upAttack++;
    }
    public void IncrementEnemiesKilled(string weaponname)
    {
        switch(weaponname)
        {
            case "sword":
                enemiesKilledSword++;
                break;
            case "axe":
                enemiesKilledAxe++;
                break;
            case "mace":
                enemiesKilledMace++;
                break;
        }
    }
}
