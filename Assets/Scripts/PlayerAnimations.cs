using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(float inputX, float inputY)
    {
        if (inputX != 0)
        {
            anim.Play("Player_WalkHorizontal");
            transform.localScale = new Vector3(-inputX, 1, 1);
        }
        else if (inputY > 0)
            anim.Play("Player_WalkUp");
        else if (inputY < 0)
            anim.Play("Player_WalkDown");
        else
            anim.Play("Player_Idle");
    }
}
