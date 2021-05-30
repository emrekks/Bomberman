using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public InputField inputField;

    public string playerName;

    private bool menuPassed = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene("Deneme");
    }
}
