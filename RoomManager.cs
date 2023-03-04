using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager singleton;

    [SerializeField] private GameObject playerPrefab;
    public Dictionary<int, int> roomCast;

    private void Awake()
    {
        singleton = this;
        roomCast = new Dictionary<int, int>();
    }

    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        
        Debug.Log("============");
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(player.Key + " " + player.Value.NickName);
        }
        Debug.Log("============");
    }

    public void CreatePlayer(Player other)
    {
        SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].player = 
            PhotonNetwork.Instantiate(playerPrefab.name, SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].position, Quaternion.Euler(25f, 45f, 25f));
    }

    public void LoadColorAndNick(Player other)
    {
        if (other == null) return;

        SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].player.GetComponent<MeshRenderer>().material.color = 
            SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].color;
        SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].nickname.text = other.NickName;
    }

    public void DestroyPlayer(Player other)
    {         
        PhotonNetwork.Destroy(SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].player);
        SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].nickname.text = "";
    }

    public void LoadRoomForLocal(Player player)
    {
        CheckAndSetOverloadPlayers(player);

        if (PhotonNetwork.IsMasterClient)
        {
            CreatePlayer(player);
            SetRoomCast();
            Debug.Log("-=-=-" + roomCast.Count);
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {     
        if (PhotonNetwork.IsMasterClient)
        {
            SetRoomCast();
            Debug.Log("-=-=-" + roomCast.Count);
        }

        CheckAndSetOverloadPlayers(other);

        if (PhotonNetwork.IsMasterClient)
        {
            CreatePlayer(other);
        }
        //LoadColorAndNick(other);
    }

    private void CheckAndSetOverloadPlayers(Player other)
    {
        if (other.ActorNumber > 4)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (!roomCast.ContainsKey(i))
                {
                    roomCast.Add(i, other.ActorNumber);
                    
                    return;
                }
            }
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DestroyPlayer(other);
        }

        SpawnPlayerManager.singleton.spawnPoints[GetPlayerId(other) - 1].nickname.text = "";
    }

    private int GetPlayerId(Player player)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);
        Debug.Log(roomCast.Count);
        
        for (int i = 1; i <= 4; i++)
        {
            if (roomCast.ContainsKey(i))
            {
                if (player.ActorNumber == roomCast[i])
                {
                    SetRoomCast();

                    return i;
                }
            }            
        }

        Debug.LogError("Not found!!!");
        return -1;
    }

    public void SetRoomCast()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (!PhotonNetwork.CurrentRoom.Players.ContainsKey(i)) continue;

            roomCast[i] = PhotonNetwork.CurrentRoom.Players[i].ActorNumber;
        }
    }
}
