using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class SpawnPlayerPoint : MonoBehaviour
{
    public enum State
    {
        Free = 0,
        Filled = 1,
    }
    
    public int id;
    public Vector3 position;
    public Color color;
    public Text nickname;
    public LayerMask playerLayerMask;
    public GameObject player;

    private void Awake()
    {
        position = transform.position;
    }

    public int SetPlayerInOverlap()
    {
        Collider[] hitColliders = Physics.OverlapBox(position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, playerLayerMask);    

        foreach (Collider collider in hitColliders)
        {            
            if (collider.gameObject.tag == "player")
            {
                player = collider.gameObject;
                return id;
            }
        }

        return -1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, new Vector3(1, 1, 1));
    }
}
