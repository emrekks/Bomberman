using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingScene : MonoBehaviourPunCallbacks
{

    #region Singleton

    public static WaitingScene instance;

    public void Awake()
    {
        instance = this;
    }

    #endregion

    public int _readyPlayerCount;
    public Text readyDisplayText;
    public Text numbersText;
    public GameObject numbers;
    public bool numbersbool = true;
    public int randomScene;
    public bool readyToPlay = false;

    private PhotonView pv;

    public GameObject ready;
    public GameObject unready;


    private float timer;
    public int numberCount = 3;
    public bool ReadyToCount = false;
    public bool GameStarting = false;
    

    void Start()
    {
        pv = GetComponent<PhotonView>();
        GameWait();
        randomScene = UnityEngine.Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        //_readyPlayerCount = _readyPlayerCount;
        readyDisplayText.text = _readyPlayerCount + " / " + PhotonRoom.room.playersInRoom;

        if (ReadyToCount == true)
        {
            if (numbers.activeSelf && readyToPlay)
            {
                pv.RPC("CountNumbers", RpcTarget.All, numberCount);
            }
        }
    }

    public void CountTrigger()
    {
        GameWait();
        pv.RPC("NumbersActive", RpcTarget.All, true);
        ReadyToCount = true;
    }




    void GameWait()
    {
        Debug.Log("GameWait");

        if (_readyPlayerCount == PhotonRoom.room.playersInRoom && PhotonRoom.room.playersInRoom != 0)
        {
            readyToPlay = true;
        }
        else
        {
            readyToPlay = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameWait();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameWait();
    }


    public void CountReadyPlayer()
    {
        pv.RPC("CountReadyPlayerRpc", RpcTarget.All, _readyPlayerCount);
    }

    [PunRPC]
    public void CountReadyPlayerRpc(int count)
    {
        _readyPlayerCount = count;
    }

    [PunRPC]
    public void NumbersActive(bool a)
    {
        if (a)
        {
            numbers.SetActive(true);
        }
    }

    [PunRPC]
    public void CountNumbers(int number)
    {
        numberCount = number;

        timer += Time.deltaTime;

        numbersText.text = Convert.ToString(number);

        if (timer >= 1f)
        {
            numbersText.fontSize = Convert.ToInt32(Mathf.Lerp(120f, 150f, 3f));
            numberCount -= 1;
            timer = 0f;
                if (number <= 0)
                {
                    numbersText.text = "GO";
                    GameStarting = true;
                    readyToPlay = false;
                    ReadyToCount = false;
                    PhotonNetwork.Destroy(this.gameObject);
                    PhotonNetwork.LoadLevel("Scene1");
                    PhotonRoom.room.playersInRoom = PhotonRoom.room.deadPlayer;
                }
        }
    }
}
