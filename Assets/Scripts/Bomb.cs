using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    Animator anim;
    public UnityEvent OnExplode;

    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("PlayBombAnimation", 1f);
    }

    void PlayBombAnimation()
    {
        anim.Play("Bomb_Explosion");
        Invoke("Explode", 2f);
    }

    void Explode()
    {
        OnExplode.Invoke();
        Destroy(gameObject, 1);
    }

}