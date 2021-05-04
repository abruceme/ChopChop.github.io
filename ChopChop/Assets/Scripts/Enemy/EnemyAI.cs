using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public float totalTime = 2f;
    protected float startTime;

    //public int health;

    //public int enemyDamage;
    
    public EnemyController enemy;

    private float moveTime = 0f;
    


    void Start()
    {
        enemy.SetWeapon();
        if(enemy.curState == EnemyController.EnemyStates.RUNNING)
        {
            enemy.Run();
        }
    }
    void Update()
    {
        /*if (enemy.curState != EnemyController.EnemyStates.RUNNING)
        {
            moveTime -= Time.deltaTime;
            if (moveTime <= 0
                &&!(enemy.Attacking() || enemy.Reacting()))
            {
                PerformMove();
            }
        }*/
    }


    public void ResetMoveTimer()
    {
        //Reset time for next move between 1 to 3s
        moveTime = Random.Range(1f, 1.5f);
        enemy.curState = (EnemyController.EnemyStates)Random.Range(0, 7);
        Debug.Log($"{this.gameObject.name}'s next Move in {moveTime}s");
    }

    public void PerformMove()
    {
        switch (enemy.curState)
        {
            case EnemyController.EnemyStates.IDLE:
                enemy.Idle();
                break;
            case EnemyController.EnemyStates.LEFTATTACK:
                enemy.LeftAttack();
                break;
            case EnemyController.EnemyStates.RIGHTATTACK:
                enemy.RightAttack();
                break;
            case EnemyController.EnemyStates.UPATTACK:
                enemy.UpAttack();
                break;
            case EnemyController.EnemyStates.LEFTDEFEND:
                enemy.LeftBlock();
                break;
            case EnemyController.EnemyStates.UPDEFEND:
                enemy.UpBlock();
                break;
            case EnemyController.EnemyStates.RIGHTDEFEND:
                enemy.RightBlock();
                break;
            default:
                break;
        }
        Debug.Log($"{gameObject.name} "+enemy.curState.ToString());
        ResetMoveTimer();
    }
    public void Spawn(Vector3 startPosition, Vector3 endPosition)
    {
        enemy.curState = EnemyController.EnemyStates.RUNNING;
        StartCoroutine(MoveTowards(startPosition, endPosition));
    }

    IEnumerator MoveTowards(Vector3 startPosition, Vector3 endPosition)
    {
        startTime = Time.time;
        while (Time.time - startTime < totalTime)
        {
            float timeElapsed = Time.time - startTime;
            float percent = timeElapsed / totalTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, percent);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = endPosition;

        Debug.Log($"{gameObject.name} Active!");
        enemy.Idle();
        ResetMoveTimer();
    }

    /*void Attack()
    {
        //Debug.Log($"{gameObject.name} Attacks with {enemyDamage}");
        //TO DO Write Attack logic
    }

    void Defend()
    {
        Debug.Log($"{gameObject.name} Defends");
        //TO DO Write Defend logic
    }

    void Damage(int damage)
    {
        if (enemy.curState == EnemyController.EnemyStates.RUNNING)
            return;
        health -= damage;
        if (health <= 0)
        {
            Debug.Log($"Enemy Dead : {gameObject.name}");
            gameObject.SetActive(false);
        }
    }*/
}
