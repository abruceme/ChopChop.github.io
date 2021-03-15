using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using scoring;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int characterHealth;
    [SerializeField]
    private Transform gameCharacter;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private GameObject damageParticle;
    private ChopChopAnalytics chopAnalytics;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(characterHealth);

        GameObject go = GameObject.Find("ChopChopAnalytics");
        if (go != null)
        {
            chopAnalytics = go.GetComponent<ChopChopAnalytics>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TakeDamage(int damage)
    {
        characterHealth -= damage;
        healthBar.SetHealth(characterHealth);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "weapon")
        {
            WeaponCollision opponentWeapon = other.gameObject.GetComponent<WeaponCollision>();
            Animator opponentAnimator = opponentWeapon.defenderAnimator;

            string opponentTag = opponentAnimator.gameObject.tag;
            string characterTag = gameCharacter.tag;
            int move = opponentAnimator.GetInteger("Move");
            bool opponentCanDamage = opponentAnimator.gameObject.GetComponent<GameCharacterController>().CanDamage();
            //Debug.Log("character: " + opponentTag + "; canDamage: "+opponentCanDamage);
            if (characterTag != opponentTag
                && IsAttack(move)
                && WeaponColliderMatch(opponentWeapon, move)
                && opponentCanDamage)
            {
                Instantiate(damageParticle, other.contacts[0].point, Quaternion.identity);
                TakeDamage(opponentWeapon.weaponDamage);
                Debug.Log(characterTag + " Health: " + characterHealth);



                if (opponentTag == "Player")
                {
                    opponentWeapon.DamageWeapon();
                    ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.enemydamaged);
                    Score.addScore(0, 15);
                }
                else
                {
                    GameCharacterController pcontroller = this.gameObject.transform.parent.parent.parent.GetComponentInParent<GameCharacterController>();
                    if (pcontroller.Blocking())
                    {
                        ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.failedToBlock);
                    }
                }
            }
            if (characterHealth < 1)
            {

                if (characterTag == "Enemy")
                {
                    Destroy(gameCharacter.parent.gameObject);
                    gameCharacter.GetComponentInParent<EnemySpawner>().ResetTimeBetweenNextSpawn();
                    ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.enemiesKilledSword, opponentWeapon.gameObject.name);
                    Score.addScore(5, 25);
                }
                if (characterTag == "Player")
                {
                    Time.timeScale = 0f;
                    GameObject.Find("PauseButton").SetActive(false);
                    GameObject.Find("Canvas").transform.Find("Restart").gameObject.SetActive(true);
                    ChopChopAnalytics.RunAnalytics(chopAnalytics, ChopChopAnalytics.functiontype.currenttime);
  

                }

                Debug.Log(characterTag + " died :(");


            }
        }
    }

    private bool IsAttack(int move)
    {
        return IsSlash(move) || IsPunch(move);
    }
    private bool IsSlash(int move)
    {
        return move == (int)GameCharacterController.CharacterStates.LEFTSLASH
            || move == (int)GameCharacterController.CharacterStates.RIGHTSLASH
            || move == (int)GameCharacterController.CharacterStates.UPSLASH;
    }
    private bool IsPunch(int move)
    {
        return move == (int)GameCharacterController.CharacterStates.LEFTPUNCH
            || move == (int)GameCharacterController.CharacterStates.RIGHTPUNCH
            || move == (int)GameCharacterController.CharacterStates.UPPUNCH;
    }

    private bool WeaponColliderMatch(WeaponCollision weapon, int move)
    {
        return (weapon.IsWeapon()
            && IsSlash(move))
            || (!weapon.IsWeapon()
            && IsPunch(move));
    }
}
