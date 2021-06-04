using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoints;
    public int inWaitingRoomPlayerCount;
    private void Awake()
    {
        
    }
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
        _roomOption.PublishUserId = true;
        PhotonNetwork.JoinOrCreateRoom("Room_1", _roomOption, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CountOfPlayers == 1)
        {
            inWaitingRoomPlayerCount = 0;
        }
        if (PhotonNetwork.CountOfPlayers == 2)
        {
            inWaitingRoomPlayerCount = 1;
        }
        if (PhotonNetwork.CountOfPlayers == 3)
        {
            inWaitingRoomPlayerCount = 3;
        }
        if (PhotonNetwork.CountOfPlayers == 4)
        {
            inWaitingRoomPlayerCount = 4;
        }
        Debug.Log("join");
        PhotonNetwork.Instantiate("Player", spawnPoints[inWaitingRoomPlayerCount].position, Quaternion.identity, 0, null);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("A player left");
    }

}
