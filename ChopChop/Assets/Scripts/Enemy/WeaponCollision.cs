using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "weapon")
        {

            int currentMove = collision.collider.gameObject.GetComponent<Animator>().GetInteger("Move");
            if (currentMove > 2)
            {
                Debug.Log("Move Integer: " + currentMove);
                Animator animator = gameObject.GetComponent<Animator>();
                animator.SetInteger("Move", animator.GetInteger("Move") - 3);
            }
        }
    }


}
