using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    #region Singleton

    public static MainMenu instance;

    public void Awake()
    {
        instance = this;
    }

    #endregion

    public InputField inputField;

    public string playerName;

    private bool menuPassed = false;

    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
        

        if (!menuPassed)
        {
            playerName = inputField.text;
        }
        
        
        
        
        
    }

    public void OnClick()
    {
        menuPassed = true;
        SceneManager.LoadScene("WaitingRoom");
    }
}
