using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MyNetworkManager : NetworkManager
{
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPoint;
        
        if(numPlayers == 0)
        {
            startPoint = player1SpawnPoint;
        }
        else
        {
            startPoint = player2SpawnPoint;
        }
        
        GameObject new_player = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);

        NetworkServer.AddPlayerForConnection(conn, new_player);
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        Debug.Log("Seja bem vindo!");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        
        Debug.Log("Encerrando o Server...");
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        
        Debug.Log("Novo jogador conectado!");
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        
        Debug.Log("Um jogador saiu da partida...");
    }

}
