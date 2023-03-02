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

    public SpawnPlayerPoint GetFreePoint()
    {
        foreach (SpawnPlayerPoint point in spawnPoints)
        {
            if (point.freeState == SpawnPlayerPoint.State.Free)
            {
                return point;
            }
        }

        return null;
    }
}
