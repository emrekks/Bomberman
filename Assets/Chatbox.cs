using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Chatbox : MonoBehaviour
{

    public Text InputField;
    public Text ChatBox;
    public PhotonView pw;
    [SerializeField] string message;

    private MainMenu _mainMenu;

    

    public void Start()
    {
        _mainMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainMenu>();
        pw = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<PhotonView>();
        InputField = GameObject.Find("WriteChat").GetComponent<Text>();
        ChatBox = GameObject.Find("ChatText").GetComponent<Text>();
    }
    public void SendMessageToChat()
    {
        message = InputField.text;
        pw.RPC("ShowInChat", RpcTarget.AllBuffered, message,_mainMenu.playerName);
        InputField.text = string.Empty;

    }

    [PunRPC]
    public void ShowInChat(string message, string playerName)
    {
        ChatBox.text += $"\n{playerName}: {message}";
        //ChatBox.text += $"\n {pw.ViewID}: {message}";
    }

}
