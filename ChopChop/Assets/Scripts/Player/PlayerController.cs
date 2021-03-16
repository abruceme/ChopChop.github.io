using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class PlayerController : GameCharacterController
{


    private const float minimumHeldDuration = 0.2f;
    private float keyPressedTime = 0;
    private bool keyHeld = false;
    private KeyCode keyPressed = KeyCode.None;
    private bool canBlock = true;
    
    private ChopChopAnalytics chopAnalytics;


    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(GameCharacterController.WeaponStates.NOWEAPON);
        // SetPlayerWeapon();
        GameObject go = GameObject.Find("ChopChopAnalytics");
        if (go != null)
        {
            chopAnalytics = go.GetComponent<ChopChopAnalytics>();
        }
    }

    //for drinking powerpotion
    public void addAttack(){
        SetPlayerWeapon();
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

    private void Block()
    {
        if (canBlock)
        {
            switch (keyPressed)
            {
                case KeyCode.A:
                    //Debug.Log("A held down");
                    LeftBlock();
                    break;
                case KeyCode.W:
                    //Debug.Log("W held down");
                    UpBlock();
                    break;
                case KeyCode.D:
                    //Debug.Log("D held down");
                    RightBlock();
                    break;
            }
        }
    }
    private void Attack()
    {
        switch (keyPressed)
        {
            case KeyCode.A:
                //Debug.Log("A pressed");
                LeftAttack();
                ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.leftAttack);
                break;
            case KeyCode.W:
                //Debug.Log("W pressed");
                UpAttack();
                ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.upAttack);

                break;
            case KeyCode.D:
                //Debug.Log("D pressed");
                RightAttack();
                ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.rightAttack);
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
    public bool CanBlock()
    {
        return canBlock;
    }
    public void DisableBlock()
    {
        canBlock = false;
    }
    public void EnableBlock()
    {
        canBlock = true;
    }

}
