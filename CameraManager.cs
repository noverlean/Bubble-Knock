using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager singleton;
        
    public enum CameraTypes
    {
        Menu = 0,
        Lobby = 1,
        Game = 2,
    }

    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;
    [SerializeField] private List<CameraTypes> cameraTypes;
    private Dictionary<CameraTypes, CinemachineVirtualCamera> cameras = new Dictionary<CameraTypes, CinemachineVirtualCamera>();

    private CameraTypes activeCameraType;

    private void Start()
    {
        singleton = this;

        ConfigureCamerasDictionary();
    }

    private void ConfigureCamerasDictionary()
    {
        for (int i = 0; i < cameraTypes.Count; i++)
        {
            cameras.Add(cameraTypes[i], virtualCameras[i]);
        }
    }

    public void ChangeActiveCamera(CameraTypes type)
    {
        activeCameraType = type;

        foreach (KeyValuePair<CameraTypes, CinemachineVirtualCamera> camera in cameras)
        {
            camera.Value.Priority = 0;
        }

        cameras[type].Priority = 1;
    }
}
