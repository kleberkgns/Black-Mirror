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

    public int coins;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(inputX, inputY) * speed;
        }

            
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pedindo uma mensagem para o Server!");
            TalkToAll();
        }
    }

    //------------------------------- Comunicação entre Cliente e Servidor--------------------------------

    [Command]
    void TalkToServer()
    {
        Debug.Log("Player pediu uma mensagem!");
    }

    [ClientRpc]
    void TalkToAll()
    {
        Debug.Log("E aí galerinha que assiste meu canal!");
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            coins++;
            MyNetworkManager.spawnedCoins--;
            Destroy(collision.gameObject);
        }
    }
}
