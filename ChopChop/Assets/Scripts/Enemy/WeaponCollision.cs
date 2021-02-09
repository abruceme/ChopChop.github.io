using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
            if (IsSlashMove(currentMove))
            {
                Debug.Log("Move Integer: " + currentMove);
                ReturnToHold();
            }
        }
    }
    void ReturnToHold()
    {
        animator.SetInteger("Move", animator.GetInteger("Move") - 3);
    }
    private bool IsSlashMove(int move)
    {
        return move == 4 || move == 5 || move == 6;
    }

}
