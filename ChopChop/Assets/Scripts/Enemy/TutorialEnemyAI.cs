using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutorialEnemyAI : EnemyAI
{
    [HideInInspector]
    public enum TutorialStates
    {
        ATTACK,
        STEAL,
        BLOCK,
        FREE
    }
    private Transform tutorialText;
    private Transform nextButton;
    private Transform tutorialPanel;
    public TutorialStates curTutState = TutorialStates.ATTACK;
    void Start()
    {
        enemy.SetWeapon();
        if (enemy.curState == EnemyController.EnemyStates.RUNNING)
        {
            enemy.Run();
        }
        tutorialText = GameObject.Find("Canvas").transform.Find("TutorialText");
        nextButton = GameObject.Find("Canvas").transform.Find("TutorialNextButton");
        tutorialPanel = GameObject.Find("Canvas").transform.Find("TutorialPanel");
    }
    public new void ResetMoveTimer()
    {
        switch (curTutState)
        {
            case TutorialStates.ATTACK:
                enemy.curState = EnemyController.EnemyStates.IDLE;
                break;
            case TutorialStates.STEAL:
            case TutorialStates.BLOCK:
                enemy.curState = (EnemyController.EnemyStates)Random.Range(1, 4);
                break;
            case TutorialStates.FREE:
                enemy.curState = (EnemyController.EnemyStates)Random.Range(0, 7);
                break;
        }
        Debug.Log("In state: " + Enum.GetName(typeof(TutorialStates), (int)curTutState));
        Debug.Log($"{this.gameObject.name}'s next Tutorial Move");
    }
    public new void PerformMove()
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
        Debug.Log($"{gameObject.name} " + enemy.curState.ToString());
        ResetMoveTimer();
    }
    public new void Spawn(Vector3 startPosition, Vector3 endPosition)
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
        SwitchText();
    }
    public void IncCurTutorialState()
    {
        curTutState = (TutorialStates)((int)curTutState + 1);
        SwitchText();
        ResetMoveTimer();
    }
    private void SwitchText()
    {
        tutorialText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        tutorialPanel.gameObject.SetActive(true);
        switch (curTutState)
        {
            case TutorialStates.ATTACK:
                tutorialText.GetComponent<TMPro.TextMeshProUGUI>().text = "WELCOME TO THE TUTORIAL! \n Let's start with offense. Pressing w (up),a (left), or d (right) will allow you to attack in that direction.";
                break;
            case TutorialStates.BLOCK:
                tutorialText.GetComponent<TMPro.TextMeshProUGUI>().text = "You have a weapon! Now defend yourself! \n Holding w (up),a (left), or d (right) will allow you to block in that direction. \n Block in the direction of the attack!";
                break;
            case TutorialStates.STEAL:
                tutorialText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's get a weapon. \n Holding w (up),a (left), or d (right) will allow you to steal in that direction. \n React right before the opponent hits to steal!";
                break;
            case TutorialStates.FREE:
                tutorialText.GetComponent<TMPro.TextMeshProUGUI>().text = "You did it! \n Feel free to stay here. \n Go to PRACTICE mode to practice what you learned! \n Go to ENDLESS mode to fight!";
                break;
        }
        Time.timeScale = 0f;
    }
}
