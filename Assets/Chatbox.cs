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

    public void Start()
    {
        pw = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<PhotonView>();
        InputField = GameObject.Find("WriteChat").GetComponent<Text>();
        ChatBox = GameObject.Find("ChatText").GetComponent<Text>();
    }
    public void SendMessageToChat()
    {
        message = InputField.text;
        pw.RPC("ShowInChat", RpcTarget.All, message);
        InputField.text = string.Empty;
        
    }

    [PunRPC]
    public void ShowInChat(string message)
    {
        ChatBox.text += $"\n {pw.ViewID}: {message}";
    }

}
