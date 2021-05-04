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
    private int stealweapon = 0;
    private int boost1 = 0;
    private int boost2 = 0;
    private string currentTime;
    [HideInInspector]
    public enum functiontype
    {
        enemydamaged,
        failedToBlock,
        attackBlocked,
        enemiesKilledSword,
        enemiesKilledAxe,
        enemiesKilledMace,
        leftAttack,
        rightAttack,
        upAttack,
        currenttime,
        stealweapon,
        boost1,
        boost2
    }

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

        AnalyticsResult result = Analytics.CustomEvent("AvgGameSession", new Dictionary<string, object>{
            {"mTimeTaken", currentTime},
            {"EnemyDamaged", enemydamaged},
            {"BlockFails", failedToBlock},
            {"BlockSuccess", attackBlocked},
            {"LeftAttack", leftAttack},
            {"RightAttack", rightAttack},
            {"UpAttack", upAttack},
            {"EnemiesKilledBySword", enemiesKilledSword },
            {"EnemiesKilledByAxe", enemiesKilledAxe },
            {"EnemiesKilledByMace", enemiesKilledMace }//,
            //{"WeaponStolen", stealweapon }
        });
        Debug.Log("Result: " + result);

        AnalyticsResult result2 = Analytics.CustomEvent("AvgGameSession2", new Dictionary<string, object>{
            {"WeaponStolen", stealweapon},
            {"HealthBoostUsed", boost1 },
            {"DamageBoostUsed", boost2 }
        });
        Debug.Log("Result: " + result2);

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
        switch (weaponname)
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
    public void IncrementSteal()
    {
        stealweapon++;
    }

    public void IncrementBoost1()
    {
        boost1++;
    }

    public void IncrementBoost2()
    {
        boost2++;
    }

    public static void RunAnalytics(ChopChopAnalytics chop, functiontype func, string weaponname = null)
    {
        if (chop != null)
        {
            switch (func)
            {
                case functiontype.enemydamaged:
                    chop.IncrementEnemyDamaged();
                    break;
                case functiontype.failedToBlock:
                    chop.IncrementBlockFailed();
                    break;
                case functiontype.attackBlocked:
                    chop.IncrementBlockSuccess();
                    break;
                case functiontype.enemiesKilledSword:
                    chop.IncrementEnemiesKilled(weaponname);
                    break;
                case functiontype.leftAttack:
                    chop.IncrementLeftAttack();
                    break;
                case functiontype.rightAttack:
                    chop.IncrementRightAttack();
                    break;
                case functiontype.upAttack:
                    chop.IncrementUpAttack();
                    break;
                case functiontype.currenttime:
                    chop.TimeTrack();
                    break;
                case functiontype.stealweapon:
                    chop.IncrementSteal();
                    break;
                case functiontype.boost1:
                    chop.IncrementBoost1();
                    break;
                case functiontype.boost2:
                    chop.IncrementBoost2();
                    break;
            }
        }
    }
}
