using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum Panels
{
    CreatePrivateRoom = 0,
    SetNickname = 1,
}

public class MenuLinker : MonoBehaviour
{
    [Header("Main menu buttons")]
    [SerializeField] private Button playMatchMaking;
    [Space]
    [Header("Create private room panel's buttons")]
    [SerializeField] private Button openCreatePrivateRoomPanel;
    [SerializeField] private Button closeCreatePrivateRoomPanel;
    [SerializeField] private Button connectToPrivateRoom;
    [Space]
    [Header("Set nickname panel's buttons")]
    [SerializeField] private Button openSetNicknamePanel;
    [SerializeField] private Button closeSetNicknamePanel;
    [SerializeField] private Button setNickname;
    [Space]
    [Header("Panels")]
    [SerializeField] private List<Panels> panelsKeys;
    [SerializeField] private List<Panel> panelsObjects;
    private Dictionary<Panels, Panel> panels = new Dictionary<Panels, Panel>();
    [Space]
    [Header("Set nickname panel's buttons")]
    [SerializeField] private Button leaveRoom;
    [SerializeField] private Button allReady;
    [Space]
    [Header("Other")]
    [SerializeField] private InputField privateRoomName;
    [SerializeField] private InputField nicknameField;

    private void Start()
    {
        ConfigurePanelsDictionary();
        LinkButtons();
    }

    private void ConfigurePanelsDictionary()
    {
        for (int i = 0; i < panelsKeys.Count; i++)
        {
            panels.Add(panelsKeys[i], panelsObjects[i]);
        }
    }

    private void LinkButtons()
    {
        playMatchMaking.onClick.AddListener(() => ConnectionManager.singleton.PlayMatchMaking());

        openCreatePrivateRoomPanel.onClick.AddListener(panels[Panels.CreatePrivateRoom].Open);
        closeCreatePrivateRoomPanel.onClick.AddListener(panels[Panels.CreatePrivateRoom].Close);
        connectToPrivateRoom.onClick.AddListener(() => ConnectionManager.singleton.ConnectToPrivateRoom(privateRoomName.text));

        openSetNicknamePanel.onClick.AddListener(panels[Panels.SetNickname].Open);
        closeSetNicknamePanel.onClick.AddListener(panels[Panels.SetNickname].Close);
        setNickname.onClick.AddListener(() => Account.singleton.SetNickname(nicknameField.text));
    }
}
