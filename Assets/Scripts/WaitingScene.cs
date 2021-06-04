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
    private int roomSize = 4;
    public Text readyDisplayText;
    public Text numbersText;
    public GameObject numbers;
    public bool numbersbool = true;
    public int randomScene;
    public bool readyToPlay = false;

    private PhotonView pv;

    public GameObject ready;
    public GameObject unready;

    private GameObject[] players;

    private float timer;
    private int numberCount = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        GameWait();
        randomScene = UnityEngine.Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        roomSize = players.Length;
        readyDisplayText.text = _readyPlayerCount + " / " + roomSize;
        WaitingToReady();

        if (numbers.activeSelf && readyToPlay)
        {
            timer += Time.deltaTime;
            
            numbersText.text = Convert.ToString(numberCount);
            

            if (timer >= 1f)
            {
                numbersText.fontSize = Convert.ToInt32(Mathf.Lerp(120f, 150f, 3f));
                numberCount -= 1;
                pv.RPC("CountNumbers", RpcTarget.All, numberCount);
                timer = 0f;

                if (numberCount <= 0)
                {
                    numbersText.text = "GO";
                    readyToPlay = false;
                    //RANDOM SCENE SEÇMECE
                    if(randomScene == 0)
                    {
                        SwitchLevel("Scene2");
                    }
                    else
                    {
                        SwitchLevel("Scene2");
                    }
                    //UILARIN HEPSININ KAPANMASI LAZIM CHAT SAYILAR VS..
                }
            }
        }
    }

    public void SwitchLevel(string level)
    {
        StartCoroutine(DoSwitchLevel(level));
    }

    IEnumerator DoSwitchLevel(string level)
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        yield return null;
        PhotonNetwork.LoadLevel(level);
    }

    void GameWait()
    {
        Debug.Log("GameWait");


        if (_readyPlayerCount == roomSize)
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

        // if (PhotonNetwork.IsMasterClient)
        // {
        //     pv.RPC();
        // }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameWait();
    }

    void WaitingToReady()
    {
        if (readyToPlay)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        PhotonNetwork.CurrentRoom.IsOpen = false;
        pv.RPC("NumbersActive", RpcTarget.AllBuffered, numbersbool);
    }


    public void CountReadyPlayer()
    {
        pv.RPC("CountReadyPlayerRpc", RpcTarget.AllBuffered, _readyPlayerCount);
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
    void CountNumbers(int number)
    {
      numberCount = number;
    }
}
