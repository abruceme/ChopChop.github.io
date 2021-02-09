using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int characterHealth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "weapon")
        {
            string characterTag = other.transform.parent.parent.tag;
            int move = other.GetComponent<Animator>().GetInteger("Move");
            
            string thisTag = this.transform.parent.tag;
            int damage = 0;
            if (characterTag == "Enemy" && thisTag != "Enemy" && IsSlashMove(move))
            {
                damage = 5;
            }
            else if (characterTag == "Player" && thisTag != "Player")
            {
                damage = 5;
            }
            characterHealth -= damage;
            Debug.Log(thisTag + " Health: " + characterHealth);
            if (characterHealth < 1)
            {
                Destroy(this.transform.parent.gameObject);
                if(thisTag == "Enemy")
                {
                    this.transform.parent.parent.GetComponentInParent<EnemySpawner>().ResetTimeBetweenNextSpawn();
                }
            }
        }
    }

    private bool IsSlashMove(int move)
    {
        return move == 4 || move == 5 || move == 6;
    }
}
