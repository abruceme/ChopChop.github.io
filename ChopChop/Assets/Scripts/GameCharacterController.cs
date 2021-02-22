using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterController : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    public WeaponStates currentWeapon = WeaponStates.NOWEAPON;
    public Transform weapon;
    public enum WeaponStates
    {
        NOWEAPON,
        SWORD,
        MACE,
        AXE
    }
    public enum CharacterStates
    {
        WEAPONLESSIDLE,
        LEFTPUNCHHOLD,
        UPPUCHHOLD,
        RIGHTPUNCHHOLD,
        LEFTPUNCH,
        UPPUNCH,
        RIGHTPUNCH,
        LEFTPARRY,
        UPPARRY,
        RIGHTPARRY,
        WEAPONIDLE,
        LEFTSLASHHOLD,
        UPSLASHHOLD,
        RIGHTSLASHHOLD,
        LEFTSLASH,
        UPSLASH,
        RIGHTSLASH,
        LEFTBLOCK,
        UPBLOCK,
        RIGHTBLOCK,
        PARRYREACTION,
        RUNNING
    }


    public void Idle()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.WEAPONLESSIDLE);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.WEAPONIDLE);
        }
    }
    public void LeftAttack()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.LEFTPUNCH);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.LEFTSLASH);
        }
    }
    public void RightAttack()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.RIGHTPUNCH);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.RIGHTSLASH);
        }
    }
    public void UpAttack()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.UPPUNCH);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.UPSLASH);
        }
    }
    public void LeftBlock()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.LEFTPARRY);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.LEFTBLOCK);
        }
    }
    public void UpBlock()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.UPPARRY);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.UPBLOCK);
        }
    }
    public void RightBlock()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)CharacterStates.RIGHTPARRY);
        }
        else
        {
            animator.SetInteger("Move", (int)CharacterStates.RIGHTBLOCK);
        }
    }
    public void Run()
    {
        animator.SetInteger("Move", (int)CharacterStates.RUNNING);
    }
    public bool Blocking()
    {
        if (animator.GetInteger("Move") == (int)CharacterStates.LEFTPARRY
        || animator.GetInteger("Move") == (int)CharacterStates.UPPARRY
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTPARRY
        || animator.GetInteger("Move") == (int)CharacterStates.LEFTBLOCK
        || animator.GetInteger("Move") == (int)CharacterStates.UPBLOCK
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTBLOCK)
        {
            return true;
        }
        return false;

    }
    public bool Chilling()
    {
        if (animator.GetInteger("Move") == (int)CharacterStates.WEAPONLESSIDLE
        || animator.GetInteger("Move") == (int)CharacterStates.WEAPONIDLE)
        {
            return true;
        }
        return false;
    }
    public void SetWeapon()
    {
        DeactivateAllWeapons();
        int nextMove = (int)CharacterStates.WEAPONLESSIDLE;
        switch (currentWeapon)
        {
            case WeaponStates.SWORD:
                weapon.Find("sword").gameObject.SetActive(true);
                nextMove = (int)CharacterStates.WEAPONIDLE;
                break;
            case WeaponStates.AXE:
                weapon.Find("axe").gameObject.SetActive(true);
                nextMove = (int)CharacterStates.WEAPONIDLE;
                break;
            case WeaponStates.MACE:
                weapon.Find("mace").gameObject.SetActive(true);
                nextMove = (int)CharacterStates.WEAPONIDLE;
                break;
        }
        animator.SetInteger("Move", nextMove);
    }
    public void DeactivateAllWeapons()
    {
        int numOfWeapons = weapon.childCount;
        for (int i = 0; i < numOfWeapons; i++)
        {
            weapon.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void SetWeapon(GameCharacterController.WeaponStates item)
    {
        currentWeapon = item;
        SetWeapon();
    }

}
