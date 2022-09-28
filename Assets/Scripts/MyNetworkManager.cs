using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    public List<Transform> coinSpawnPoints;
    public int maxCoinsInGame = 2;
    public static int spawnedCoins = 0;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPoint;
        Color color;

        if(numPlayers == 0)
        {
            startPoint = player1SpawnPoint;
            color = Color.green;
        }
        else
        {
            startPoint = player2SpawnPoint;
            InvokeRepeating("SpawnCoin", 2, 2);
            color = Color.red;
        }
        
        GameObject new_player = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);

        new_player.GetComponent<Player>().playerColor = color;

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

        
    public void SpawnCoin()
    {
        if(spawnedCoins < maxCoinsInGame)
        {
            Vector3 local = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)].position;
            
            GameObject new_coin = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Moneda"), local, transform.rotation);
                    
            NetworkServer.Spawn(new_coin);
            spawnedCoins++;
        }
    }
}
