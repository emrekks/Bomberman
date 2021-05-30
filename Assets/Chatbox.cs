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
    // Start is called before the first frame update
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
