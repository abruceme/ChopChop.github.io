using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
        chopAnalytics = GameObject.Find("ChopChopAnalytics").GetComponent<ChopChopAnalytics>();
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
            if (characterTag != opponentTag
                && IsAttack(move)
                && WeaponColliderMatch(opponentWeapon,move))
            {
                Instantiate(damageParticle, other.contacts[0].point, Quaternion.identity);
                TakeDamage(opponentWeapon.weaponDamage);
                Debug.Log(characterTag + " Health: " + characterHealth);

                

                if (opponentTag == "Player")
                {
                    opponentWeapon.DamageWeapon();
                    chopAnalytics.IncrementEnemyDamaged();
                }
                else 
                {
                    GameCharacterController pcontroller = this.gameObject.transform.parent.parent.parent.GetComponentInParent<GameCharacterController>();
                    if (pcontroller.Blocking())
                    {
                        chopAnalytics.IncrementBlockFailed();
                    }
                }
            }
            if (characterHealth < 1)
            {
                
                if (characterTag == "Enemy")
                {
                    Destroy(gameCharacter.parent.gameObject);
                    gameCharacter.GetComponentInParent<EnemySpawner>().ResetTimeBetweenNextSpawn();
<<<<<<< HEAD
                    chopAnalytics.IncrementEnemiesKilled(opponentWeapon.gameObject.name);


=======
                    AnalyticsResult result = Analytics.CustomEvent("Destroy", null);
                    Debug.Log(result);
>>>>>>> 79285e5f07e1932a3380c918abc253bde48d7e44
                }
                if(characterTag == "Player")
                {
                    Time.timeScale = 0f;
                    GameObject.Find("PauseButton").SetActive(false);
                    GameObject.Find("Canvas").transform.Find("Restart").gameObject.SetActive(true);
                    chopAnalytics.TimeTrack();
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
            &&IsSlash(move))
            ||(!weapon.IsWeapon()
            &&IsPunch(move));
    }
}
