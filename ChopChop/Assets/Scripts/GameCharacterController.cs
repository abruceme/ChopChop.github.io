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
        UPPUNCHHOLD,
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
        Debug.Log(tag + " idle");
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
        return animator.GetInteger("Move") == (int)CharacterStates.LEFTPARRY
        || animator.GetInteger("Move") == (int)CharacterStates.UPPARRY
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTPARRY
        || animator.GetInteger("Move") == (int)CharacterStates.LEFTBLOCK
        || animator.GetInteger("Move") == (int)CharacterStates.UPBLOCK
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTBLOCK;

    }
    public bool Chilling()
    {
        return animator.GetInteger("Move") == (int)CharacterStates.WEAPONLESSIDLE
        || animator.GetInteger("Move") == (int)CharacterStates.WEAPONIDLE;
    }
    public bool Attacking()
    {
        return animator.GetInteger("Move") == (int)CharacterStates.LEFTPUNCH
        || animator.GetInteger("Move") == (int)CharacterStates.UPPUNCH
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTPUNCH
        || animator.GetInteger("Move") == (int)CharacterStates.LEFTSLASH
        || animator.GetInteger("Move") == (int)CharacterStates.UPSLASH
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTSLASH;
    }
    public bool Reacting()
    {
        return animator.GetInteger("Move") == (int)CharacterStates.LEFTPUNCHHOLD
        || animator.GetInteger("Move") == (int)CharacterStates.UPPUNCHHOLD
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTPUNCHHOLD
        || animator.GetInteger("Move") == (int)CharacterStates.LEFTSLASHHOLD
        || animator.GetInteger("Move") == (int)CharacterStates.UPSLASHHOLD
        || animator.GetInteger("Move") == (int)CharacterStates.RIGHTSLASHHOLD
        || animator.GetInteger("Move") == (int)CharacterStates.PARRYREACTION;
    }
    public void SetWeapon()
    {
        DeactivateAllWeapons();
        int nextMove = (int)CharacterStates.WEAPONLESSIDLE;
        GameObject weaponObject = null;
        int weaponHealth = 0;
        int weaponDamage = 0;
        switch (currentWeapon)
        {
            case WeaponStates.SWORD:
                weaponObject = weapon.Find("sword").gameObject;
                weaponHealth = (int)WeaponCollision.WeaponHealth.SWORD;
                weaponDamage = (int)WeaponCollision.WeaponDamage.SWORD;
                break;
            case WeaponStates.AXE:
                weaponObject = weapon.Find("axe").gameObject;
                weaponHealth = (int)WeaponCollision.WeaponHealth.AXE;
                weaponDamage = (int)WeaponCollision.WeaponDamage.AXE;
                break;
            case WeaponStates.MACE:
                weaponObject = weapon.Find("mace").gameObject;
                weaponHealth = (int)WeaponCollision.WeaponHealth.AXE;
                weaponDamage = (int)WeaponCollision.WeaponDamage.AXE;
                break;
        }
        if (weaponObject != null)
        {
            weaponObject.SetActive(true);
            weaponObject.GetComponent<WeaponCollision>().weaponHealth = weaponHealth;
            weaponObject.GetComponent<WeaponCollision>().weaponDamage = weaponDamage;
            nextMove = (int)CharacterStates.WEAPONIDLE;
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
