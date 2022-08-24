using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Movement : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isLocalPlayer) {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(inputX, inputY) * speed;
        }
    }
}
