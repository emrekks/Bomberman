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
                timer = 0f;

                if (numberCount <= 0)
                {
                    numbersText.text = "GO";
                    readyToPlay = false;
                    //RANDOM SCENE SEÇMECE
                    if(randomScene == 0)
                    {
                        if(PhotonNetwork.IsMasterClient){

                            PhotonNetwork.LoadLevel("Scene1");
                        }
                    }
                    else
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {

                            PhotonNetwork.LoadLevel("Scene1");
                        }
                    }
                    //UILARIN HEPSININ KAPANMASI LAZIM CHAT SAYILAR VS..
                }
            }
        }
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
        numbers.SetActive(true);
    }
}
