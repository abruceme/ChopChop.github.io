using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveset : MonoBehaviour
{
    Animator animator;
    public enum PlayerStates
    {
        IDLE,
        RIGHTSLASH,
        LEFTSLASH,
        RIGHTBLOCK,
        UPBLOCK,
        LEFTBLOCK
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetInteger("Move") == (int)PlayerStates.RIGHTSLASH)
        {
            animator.SetInteger("Move", (int)PlayerStates.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetInteger("Move", (int)PlayerStates.LEFTBLOCK);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetInteger("Move", (int)PlayerStates.UPBLOCK);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetInteger("Move", (int)PlayerStates.RIGHTBLOCK);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(animator.GetInteger("Move"));
            animator.SetInteger("Move", (int)PlayerStates.RIGHTSLASH);
        }
    }
}
