using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Panel : MonoBehaviour
{
    private GameObject panel;

    public void Start()
    {
        panel = gameObject;
        panel.SetActive(false);
    }

    public void Open()
    {
        panel.SetActive(true);
    }

    public void OpenNicknamePanel(InputField nicknameField)
    {
        nicknameField.text = Account.singleton.nickname;
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void SwitchState()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }
}
