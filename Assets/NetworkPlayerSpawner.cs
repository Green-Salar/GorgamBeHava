using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()
    {
        //Debug.Log("Joined room!2" + "playerID");
       // base.OnJoinedRoom();
       // THIS IS OKEY IN THE LOBBY CODE!!
        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity, 0);
        // spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(" A Player left room!");
        base.OnPlayerLeftRoom(otherPlayer);
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
