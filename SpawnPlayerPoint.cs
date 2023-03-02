using UnityEngine;
using Photon.Pun;
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
    public State freeState;
    public Color color;
    public Text nickname;
    public GameObject player;

    private void Awake()
    {
        position = transform.position;
    }

    //public void sync()
    //{
    //    view.RPC("StartPointState", RpcTarget.All, this);
    //}

    //[PunRPC]
    //public void ViewAll(SpawnPlayerPoint point)
    //{
        
    //}
}
