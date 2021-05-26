using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected master servere");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("connected lobby succ");
        RoomOptions _roomOption = new RoomOptions();
        _roomOption.MaxPlayers = 4;
        _roomOption.IsOpen = true;
        _roomOption.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("Room_1", _roomOption, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("join");
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity, 0, null);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("A player left");
    }
}
