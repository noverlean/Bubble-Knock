using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public static ConnectionManager singleton;

    private byte maxPlayersInRoom = 4;

    private void Start()
    {
        singleton = this;
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("����������� � ��������");

        while (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log(".");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�������������� � �������");
    }

    public void PlayMatchMaking()
    {
        Account.singleton.SetNetworkNickname();

        PhotonNetwork.JoinRandomRoom();
    }

    public void ConnectToPrivateRoom(string roomName)
    {
        Account.singleton.SetNetworkNickname();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = maxPlayersInRoom;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("������� �� �������!!! ������ ����");

        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersInRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("������������ � �������");        
        LoadLobby();
    }

    private void LoadLobby()
    {
        GameStageManager.singleton.ChangeGameStage(GameStageManager.Stages.InRoom);
        RoomManager.singleton.LoadRoomForLocal();
        Debug.Log("������� ���������!!!");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}
