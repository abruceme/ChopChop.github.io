using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public enum WeaponStates
    {
        NOWEAPON,
        SWORD,
        MACE,
        AXE
    }
    public enum PlayerStates
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
    private const float minimumHeldDuration = 0.2f;
    private float keyPressedTime = 0;
    private bool keyHeld = false;
    private KeyCode keyPressed = KeyCode.None;
    private Transform weapon;
    [SerializeField]
    private WeaponStates currentWeapon = WeaponStates.NOWEAPON;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        weapon = this.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/WeaponHolder");
        SetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPressed == KeyCode.None && (Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.D)))
        {
            keyPressedTime = Time.timeSinceLevelLoad;
            keyHeld = false;
            keyPressed = SetKey();
        }

        if (Input.GetKeyUp(KeyCode.A)
            && keyPressed == KeyCode.A)
        {
            if (Blocking() || keyHeld)
            {
                Idle();
            }
            else
            {
                Attack();
            }
            keyPressed = SetKey();
        }
        else if (Input.GetKeyUp(KeyCode.W)
            && keyPressed == KeyCode.W)
        {
            if (Blocking() || keyHeld)
            {
                Idle();
            }
            else
            {
                Attack();
            }
            keyPressed = SetKey();
        }
        else if (Input.GetKeyUp(KeyCode.D)
            && keyPressed == KeyCode.D)
        {
            if (Blocking() || keyHeld)
            {
                Idle();
            }
            else
            {
                Attack();
            }
            keyPressed = SetKey();
        }

        if (Input.anyKey)
        {
            if (Time.timeSinceLevelLoad - keyPressedTime > minimumHeldDuration
                && !keyHeld)
            {
                // Player has held the  key for x seconds. Consider it "held"
                keyHeld = true;
                Block();
            }
        }

    }
    private void Idle()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.WEAPONLESSIDLE);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.WEAPONIDLE);
        }
    }
    private void LeftAttack()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.LEFTPUNCH);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.LEFTSLASH);
        }
    }
    private void RightAttack()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.RIGHTPUNCH);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.RIGHTSLASH);
        }
    }
    private void UpAttack()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.UPPUNCH);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.UPSLASH);
        }
    }
    private void LeftBlock()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.LEFTPARRY);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.LEFTBLOCK);
        }
    }
    private void UpBlock()
    {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.UPPARRY);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.UPBLOCK);
        }
    }
    private void RightBlock() {
        if (currentWeapon == WeaponStates.NOWEAPON)
        {
            animator.SetInteger("Move", (int)PlayerStates.RIGHTPARRY);
        }
        else
        {
            animator.SetInteger("Move", (int)PlayerStates.RIGHTBLOCK);
        }
    }
    private void Block()
    {
        switch (keyPressed)
        {
            case KeyCode.A:
                Debug.Log("A held down");
                LeftBlock();
                break;
            case KeyCode.W:
                Debug.Log("W held down");
                UpBlock();
                break;
            case KeyCode.D:
                Debug.Log("D held down");
                RightBlock();
                break;
        }
    }
    private void Attack()
    {
        switch (keyPressed)
        {
            case KeyCode.A:
                Debug.Log("A pressed");
                LeftAttack();
                break;
            case KeyCode.W:
                Debug.Log("W pressed");
                UpAttack();
                break;
            case KeyCode.D:
                Debug.Log("D pressed");
                RightAttack();
                break;
        }
    }
    private KeyCode SetKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            return KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            return KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return KeyCode.D;
        }
        return KeyCode.None;
    }

    private bool Blocking()
    {
        if (animator.GetInteger("Move") == (int)PlayerStates.LEFTPARRY
        || animator.GetInteger("Move") == (int)PlayerStates.UPPARRY
        || animator.GetInteger("Move") == (int)PlayerStates.RIGHTPARRY
        || animator.GetInteger("Move") == (int)PlayerStates.LEFTBLOCK
        || animator.GetInteger("Move") == (int)PlayerStates.UPBLOCK
        || animator.GetInteger("Move") == (int)PlayerStates.RIGHTBLOCK)
        {
            return true;
        }
        return false;

    }
    private bool Chilling()
    {
        if (animator.GetInteger("Move") == (int)PlayerStates.WEAPONLESSIDLE
        || animator.GetInteger("Move") == (int)PlayerStates.WEAPONIDLE)
        {
            return true;
        }
        return false;
    }
    private void SetWeapon()
    {
        int numOfWeapons = weapon.childCount;
        int nextMove = (int)PlayerStates.WEAPONLESSIDLE;
        for (int i = 0; i < numOfWeapons; i++)
        {
            weapon.GetChild(i).gameObject.SetActive(false);
        }
        switch (currentWeapon)
        {
            case WeaponStates.SWORD:
                weapon.Find("sword").gameObject.SetActive(true);
                nextMove = (int)PlayerStates.WEAPONIDLE;
                break;
            case WeaponStates.AXE:
                weapon.Find("axe").gameObject.SetActive(true);
                nextMove = (int)PlayerStates.WEAPONIDLE;
                break;
            case WeaponStates.MACE:
                weapon.Find("mace").gameObject.SetActive(true);
                nextMove = (int)PlayerStates.WEAPONIDLE;
                break;
        }
        animator.SetInteger("Move", nextMove);
    }

}
