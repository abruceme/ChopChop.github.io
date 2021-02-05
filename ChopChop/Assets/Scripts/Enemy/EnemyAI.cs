using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyStates
{
    MOVING,
    ATTACK,
    IDLE,
    DEFEND
}
public class EnemyAI : MonoBehaviour
{
    public float totalTime = 5f;
    private float startTime;

    public int health;

    public int enemyDamage;
    public EnemyStates curState = EnemyStates.MOVING;

    private float moveTime = 0f;
    

    void Update()
    {
        if (curState != EnemyStates.MOVING)
        {
            moveTime -= Time.deltaTime;
            if (moveTime <= 0)
            {
                PerformMove();
            }
        }
    }


    public void ResetMoveTimer()
    {
        //Reset time for next move between 1 to 3s
        moveTime = Random.Range(5, 8);
        curState = (EnemyStates)Random.Range(1, Enum.GetNames(typeof(EnemyStates)).Length);
        Debug.Log($"{this.gameObject.name}'s next Move in {moveTime}s");
    }

    public void PerformMove()
    {
        switch(curState)
        {
            case EnemyStates.IDLE:
                break;
            case EnemyStates.ATTACK:
                Attack();
                break;
            case EnemyStates.DEFEND:
                Defend();
                break;
        }
        ResetMoveTimer();
    }
    public void Spawn(Vector3 startPosition, Vector3 endPosition)
    {
        curState = EnemyStates.MOVING;
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
        ResetMoveTimer();
    }

    void Attack()
    {
        Debug.Log($"{gameObject.name} Attacks with {enemyDamage}");
        //TO DO Write Attack logic
    }

    void Defend()
    {
        Debug.Log($"{gameObject.name} Defends");
        //TO DO Write Defend logic
    }

    void Damage(int damage)
    {
        if (curState == EnemyStates.MOVING)
            return;
        health -= damage;
        if(health <= 0)
        {
            Debug.Log($"Enemy Dead : {gameObject.name}");
            gameObject.SetActive(false);
        }
    }

    
}
