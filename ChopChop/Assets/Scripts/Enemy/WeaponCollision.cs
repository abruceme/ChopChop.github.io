using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class WeaponCollision : MonoBehaviour
{
    public Animator defenderAnimator;
    public int weaponHealth;
    public int weaponDamage;
    private ChopChopAnalytics chopAnalytics;
    public enum WeaponDamage
    {
        SWORD = 30,
        AXE = 40,
        MACE = 20
    }
    public enum WeaponHealth
    {
        SWORD = 80,
        AXE = 60,
        MACE = 100
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("ChopChopAnalytics");
        if (go != null)
        {
            chopAnalytics = go.GetComponent<ChopChopAnalytics>();
        }
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "weapon")
        {
            WeaponCollision attackerWeapon = collision.collider.gameObject.GetComponent<WeaponCollision>();
            Animator attackerAnimator = attackerWeapon.defenderAnimator;
            int attackerMove = attackerAnimator.GetInteger("Move");
            int defenderMove = defenderAnimator.GetInteger("Move");
            string defenderCharacter = defenderAnimator.gameObject.tag;
            if (IsSlashMove(attackerMove)
                && IsParryMove(defenderMove)
                && defenderCharacter == "Player"
                && DirectionMatch(attackerMove, defenderMove))
            {
                //Debug.Log(defenderCharacter + " parry! Move: " + attackerMove);
                Parry(attackerAnimator.gameObject.GetComponent<EnemyController>());
                //steal analytics here
            }
            else if (((defenderCharacter == "Enemy"
                && (IsBlockMove(defenderMove) || IsParryMove(defenderMove))
                && (IsSlashMove(attackerMove) || IsPunchMove(attackerMove))
                )
                || (defenderCharacter == "Player"
                && (NotParrying(attackerMove, defenderMove) || BlockingPunch(attackerMove, defenderMove))
                )) && DirectionMatch(attackerMove, defenderMove))
            {
                Debug.Log(defenderCharacter + " blocked! Move: " + attackerMove+ " with "+gameObject.name);
                if (defenderCharacter == "Player"
                    && IsBlockMove(defenderMove))
                {
                    DamageWeapon();
                    ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.attackBlocked);

                }
                if (defenderCharacter == "Enemy"
                    && IsSlashMove(attackerMove))
                {
                    attackerWeapon.DamageWeapon();
                }
                ReturnToHold(attackerAnimator);
            }
        }
    }
    private void ReturnToHold(Animator animator)
    {
        animator.SetInteger("Move", animator.GetInteger("Move") - 3);
    }
    private bool NotParrying(int attackerMove, int defenderMove)
    {
        return (IsSlashMove(attackerMove) || IsPunchMove(attackerMove))
                && IsBlockMove(defenderMove);
    }
    private bool BlockingPunch(int attackerMove, int defenderMove)
    {
        return IsPunchMove(attackerMove)
                && (IsBlockMove(defenderMove) || IsParryMove(defenderMove));
    }
    private void Parry(EnemyController enemy)
    {
        PlayerController controller = defenderAnimator.gameObject.GetComponent<PlayerController>();
        controller.SetWeapon(enemy.currentWeapon);
        enemy.DeactivateAllWeapons();
        enemy.currentWeapon = GameCharacterController.WeaponStates.NOWEAPON;
        enemy.animator.SetInteger("Move", (int)GameCharacterController.CharacterStates.PARRYREACTION);
    }
    private bool IsSlashMove(int move)
    {
        return move == (int)GameCharacterController.CharacterStates.LEFTSLASH
            || move == (int)GameCharacterController.CharacterStates.RIGHTSLASH
            || move == (int)GameCharacterController.CharacterStates.UPSLASH;
    }
    private bool IsPunchMove(int move)
    {
        return move == (int)GameCharacterController.CharacterStates.LEFTPUNCH
            || move == (int)GameCharacterController.CharacterStates.RIGHTPUNCH
            || move == (int)GameCharacterController.CharacterStates.UPPUNCH;
    }
    private bool IsBlockMove(int move)
    {
        return move == (int)GameCharacterController.CharacterStates.LEFTBLOCK
            || move == (int)GameCharacterController.CharacterStates.RIGHTBLOCK
            || move == (int)GameCharacterController.CharacterStates.UPBLOCK;
    }
    private bool IsParryMove(int move)
    {
        return move == (int)GameCharacterController.CharacterStates.LEFTPARRY
            || move == (int)GameCharacterController.CharacterStates.RIGHTPARRY
            || move == (int)GameCharacterController.CharacterStates.UPPARRY;
    }
    private bool DirectionMatch(int attackerMove, int defenderMove)
    {
        if (attackerMove > 10)
        {
            attackerMove -= 10;
        }
        if (defenderMove > 10)
        {
            defenderMove -= 10;
        }
        if ((attackerMove + 3) == defenderMove)// || attackerMove == defenderMove)
        {
            return true;
        }
        return false;
    }
    public void DamageWeapon()
    {
        if (IsWeapon())
        {
            Debug.Log(defenderAnimator.gameObject.tag + " " + gameObject.name + " weapon health: " + weaponHealth);
            weaponHealth -= 10;
            if (weaponHealth <= 0)
            {
                PlayerController player = defenderAnimator.gameObject.GetComponent<PlayerController>();
                player.SetWeapon(GameCharacterController.WeaponStates.NOWEAPON);
                player.EnableBlock();
            }
        }
    }
    public bool IsWeapon()
    {
        return gameObject.name == "sword"
            || gameObject.name == "axe"
            || gameObject.name == "mace";
    }
}
