using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager singleton;

    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        singleton = this;
    }

    public void CreatePlayer(Player other)
    {
        SpawnPlayerPoint spawnPoint = SpawnPlayerManager.singleton.spawnPoints[other.ActorNumber - 1];

        spawnPoint.player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.Euler(25f, 45f, 25f));
        spawnPoint.player.tag = "Used";
    }

    private void LinkCreatedPlayerForOthers(Player other)
    {
        SpawnPlayerPoint spawnPoint = SpawnPlayerManager.singleton.spawnPoints[other.ActorNumber - 1];

        spawnPoint.player = GameObject.FindWithTag("Unused");
        spawnPoint.player.tag = "Used";
    }

    private void LoadColorAndNicks(Player other)
    {
        SpawnPlayerPoint spawnPoint = SpawnPlayerManager.singleton.spawnPoints[other.ActorNumber - 1];

        spawnPoint.player.GetComponent<MeshRenderer>().material.color = spawnPoint.color;
        spawnPoint.nickname.text = other.NickName;
    }

    public void DestroyPlayer(Player other)
    {
        SpawnPlayerPoint spawnPoint = SpawnPlayerManager.singleton.spawnPoints[other.ActorNumber - 1];
        PhotonNetwork.CurrentRoom.Players[other.ActorNumber] = null;

        Destroy(spawnPoint.player);
        spawnPoint.player = null;
        spawnPoint.nickname.text = "";
    }

    public void LoadRoomForLocal()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CreatePlayer(PhotonNetwork.LocalPlayer);
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            //SpawnPlayerManager.singleton.spawnPoints[player.Key - 1].player = 
            LoadColorAndNicks(player.Value);
        }      
    }

    //public void UpdateRoom(Player localPlayer)
    //{
//        for (int i = 0; i<PhotonNetwork.CurrentRoom.PlayerCount; i++)
//        {
//            if (PhotonNetwork.IsMasterClient)
//            {
//                if (SpawnPlayerManager.singleton.spawnPoints[i].player != null)
//                {
//                    DestroyPlayer(PhotonNetwork.CurrentRoom.Players[i + 1]);
//}

//if (PhotonNetwork.CurrentRoom.Players[i + 1] != null)
//{
//    CreatePlayer(PhotonNetwork.CurrentRoom.Players[i + 1]);
//}
//            }
//            else
//{
//    SpawnPlayerManager.singleton.spawnPoints[i].player = GameObject.FindWithTag("Unused");
//    SpawnPlayerManager.singleton.spawnPoints[i].player.tag = "Used";
//}

//UpdateColorsAndNamesOfPlayer(PhotonNetwork.CurrentRoom.Players[i + 1]);
//    }
//}

public override void OnPlayerEnteredRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CreatePlayer(other);
        }
        else
        {
            LinkCreatedPlayerForOthers(other);
        }

        LoadColorAndNicks(other);
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DestroyPlayer(other);
        }
    }
}
