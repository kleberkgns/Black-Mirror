using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

[Serializable]
public class IntEvent : UnityEvent<int> {}

[Serializable]
public class FloatEvent : UnityEvent<float> { }

[Serializable]
public class InputEvent : UnityEvent<float, float> { }

public class Player : NetworkBehaviour
{
    //Variables
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 3;

    [SyncVar] public int coins;
    [SyncVar] public Color playerColor;

    //Events
    public IntEvent OnCoinCollect;

    public InputEvent OnDirectionChanged;

    //Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = playerColor;
        GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddPlayerListener(this);        
    }

    void Update()
    {
        if(isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            OnDirectionChanged.Invoke(inputX, inputY);

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


    [Server]
    public void AddCoins()
    {
        coins += 1;
        OnCoinCollect.Invoke(coins);
    }

    [Server]
    public void RemoveCoins(int value)
    {
        coins -= value;
        if (coins < 0) coins = 0;
        OnCoinCollect.Invoke(coins);
    }

    //Collision Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            AddCoins();
            MyNetworkManager.spawnedCoins--;
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
