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
}
