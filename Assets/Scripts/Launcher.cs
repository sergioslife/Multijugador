using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Referencia al prefab del jugador
    public PhotonView playerPrefab;

    // Punto de spawn para instanciar al jugador
    public Transform spawnPoint;

    // M�todo llamado al iniciar el objeto
    void Start()
    {
        // Conectarse al servidor de Photon
        PhotonNetwork.ConnectUsingSettings();
    }

    // M�todo llamado cuando se conecta al servidor de Photon
    public override void OnConnectedToMaster()
    {
        // Registro de un mensaje de depuraci�n
        Debug.Log("Bienvenido");

        // Intentar unirse a una sala aleatoria o crear una si no hay disponible
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    // M�todo llamado cuando se une a una sala
    public override void OnJoinedRoom()
    {
        // Instanciar el prefab del jugador en el punto de spawn
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}

