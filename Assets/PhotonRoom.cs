using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //SpawnPoints
    public Transform[] spawnPoints;
    public GameObject[] WaitingPlayerSprite;
    public GameObject PlayerSprite;

    //Room info
    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    //Player info
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;
    public int playersInGame;

    public int numberPlayers;
    public int deadPlayer;


    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        //if (playersInGame >= 2 && isGameLoaded)
        //{
        //    if (deadPlayer <= 1)
        //    {
        //        PhotonNetwork.LoadLevel("WaitingRoom");
        //    }
        //}
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        RPC_WaitingPlayer();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("JoinRoomFail");
        base.OnJoinRoomFailed(returnCode, message);
        OnJoinedRoom();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("New player add");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        RPC_WaitingPlayer();
    }

    public void readyWhenOkay()
    {
        if(WaitingScene.instance._readyPlayerCount == playersInRoom)
        {
            WaitingScene.instance.CountTrigger();
        }
    }


    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;

        if (currentScene == MultiplayerSettings.multiplayerSetting.multiplayerScene)
        {
            isGameLoaded = true;
            if (MultiplayerSettings.multiplayerSetting.delayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_PlayerSprite();
                CheckPlayers();
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
        }
    }


    void CheckPlayers()
    {
        numberPlayers = PhotonNetwork.CountOfPlayers;

        for (int i = 0; i <= numberPlayers; i++)
        {
            if (numberPlayers > 4)
            {
                numberPlayers--;
            }
        }
    }

    [PunRPC]
    void RPC_LoadedGameScene()
    {
        //playersInGame++;
        PV.RPC("RPC_InGamePlayerCount", RpcTarget.All);
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
            Debug.Log("add one more player");
        }
    }

    [PunRPC]
    void RPC_CreatePlayer()
    {
        if (numberPlayers == 1)
        {
            PhotonNetwork.Instantiate("Player", spawnPoints[0].position, Quaternion.identity, 0);
            Debug.Log("add one more player 1");
            PV.RPC("RPC_DeadPlayerCount", RpcTarget.All);
        }

        else if (numberPlayers == 2)
        {
            PhotonNetwork.Instantiate("Player", spawnPoints[1].position, Quaternion.identity, 0);
            Debug.Log("add one more player 2");
            PV.RPC("RPC_DeadPlayerCount", RpcTarget.All);
        }

        else if (numberPlayers == 3)
        {
            PhotonNetwork.Instantiate("Player", spawnPoints[2].position, Quaternion.identity, 0);
            Debug.Log("add one more player 3");
            PV.RPC("RPC_DeadPlayerCount", RpcTarget.All);
        }

        else if (numberPlayers == 4)
        {
            PhotonNetwork.Instantiate("Player", spawnPoints[3].position, Quaternion.identity, 0);
            Debug.Log("add one more player 4");
            PV.RPC("RPC_DeadPlayerCount", RpcTarget.All);
        }

    }

    [PunRPC]
    void RPC_PlayerSprite()
    {
        PlayerSprite.SetActive(false);
    }

    [PunRPC]
    void RPC_WaitingPlayer()
    {
        for(int i = 0; i < playersInRoom; i++)
        {
            WaitingPlayerSprite[i].SetActive(true);
        }
    }


    [PunRPC]
    void RPC_DeadPlayerCount()
    {
        deadPlayer++;
    }

    [PunRPC]
    void RPC_InGamePlayerCount()
    {
        playersInGame++;
    }
}
