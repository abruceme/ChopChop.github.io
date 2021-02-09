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
            int damage = 0;
            if (characterTag == "Enemy")
            {
                damage = 5;
            }else if(characterTag == "Player")
            {
                damage = 5;
            }
            characterHealth -= damage;
            Debug.Log(characterHealth);
            if (characterHealth < 1)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
