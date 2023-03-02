using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Account : MonoBehaviour
{
    public static Account singleton;
    
    public string nickname;

    private void Start()
    {
        nickname = string.Empty;
        singleton = this;
    }

    public void SetNickname(string nickname)
    {
        this.nickname = nickname;
        Debug.Log($"��� ����� ��� - {nickname}");
    }

    public void SetNetworkNickname()
    {
        if (!string.IsNullOrEmpty(nickname))
        {
            PhotonNetwork.NickName = nickname;
            Debug.Log($"��� {nickname} �����������");
        }
        else
        {
            Debug.Log("������: ��� ������ �� ��� �����!!!");
        }
    }
}
