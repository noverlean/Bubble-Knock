using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    public static SpawnPlayerManager singleton;

    [SerializeField] public List<SpawnPlayerPoint> spawnPoints;

    private void Awake()
    {
        singleton = this;
    }
}
