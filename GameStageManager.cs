using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    public enum Stages
    {
        InMenu = 0,
        InRoom = 1,
        InGame = 2,
    }

    public static GameStageManager singleton;

    private Stages stage;

    private void Start()
    {
        singleton = this;

        ChangeGameStage(Stages.InMenu);
    }

    public void ChangeGameStage(Stages stage)
    {
        this.stage = stage;

        switch (stage)
        {
            case Stages.InMenu:
                CameraManager.singleton.ChangeActiveCamera(CameraManager.CameraTypes.Menu);
                break;
            case Stages.InRoom:
                CameraManager.singleton.ChangeActiveCamera(CameraManager.CameraTypes.Lobby);
                break;
            case Stages.InGame:
                CameraManager.singleton.ChangeActiveCamera(CameraManager.CameraTypes.Game);
                break;
        }

    }
}
