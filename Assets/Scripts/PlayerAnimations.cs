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
            anim.Play("Hero_Walk");
            transform.localScale = new Vector3(inputX, 1, 1);
        }
        else if (inputY > 0)
            anim.Play("Hero_Walk_Back");
        else if (inputY < 0)
            anim.Play("Hero_Walk_Front");
        else
            anim.Play("Hero_Idle");
    }
}