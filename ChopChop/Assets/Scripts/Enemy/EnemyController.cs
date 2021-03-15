using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GameCharacterController
{
    public enum EnemyStates
    {
        IDLE,
        LEFTATTACK,
        UPATTACK,
        RIGHTATTACK,
        LEFTDEFEND,
        UPDEFEND,
        RIGHTDEFEND,
        RUNNING
    }
    public EnemyStates curState = EnemyStates.RUNNING;
    public GameObject enemyHealthObject;
    public void PerformMove()
    {
        gameObject.GetComponentInParent<EnemyAI>().PerformMove();
    }
    public void SetAttackSpeed(float speed=.6f)
    {
        animator.SetFloat("AttackSpeed", speed);
    }
    public void IncreaseAttackSpeed(float speed)
    {
        animator.SetFloat("AttackSpeed", animator.GetFloat("AttackSpeed") + speed);
    }
    public void DecreaseAttackSpeed(float speed)
    {
        animator.SetFloat("AttackSpeed", animator.GetFloat("AttackSpeed") - speed);
    }
    public void SetCharacterHealth(int health)
    {
        enemyHealthObject.GetComponent<Health>().SetCharacterHealth(health);
    }
}
