using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        Debug.Log("1");
        
        foreach (SpawnPlayerPoint point in SpawnPlayerManager.singleton.spawnPoints)
        {
            int id = point.SetPlayerInOverlap();

            if (id != -1)
                RoomManager.singleton.LoadColorAndNick(PhotonNetwork.CurrentRoom.Players[id]);
        }

        Debug.Log("3");
    }
}
