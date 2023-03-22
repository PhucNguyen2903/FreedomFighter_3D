using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using ExitGames.Client.Photon;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
   
    bool isConnected;
    ChatClient chatClient;
    public string username;
    [SerializeField] GameObject chatPanel;
    [SerializeField] string currentChat;
    string privateReceiver = "";
    [SerializeField] TMP_InputField chatField;
    [SerializeField] TextMeshProUGUI chatDisPlay;
   


    //public void UsernameOnvalueChange(string valueIn)
    //{
       
    //}
    public void SetUserName(string playerName)
    {
        username = playerName;
    }

    public void ChatConnectOnClick()
    {
       
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log(username);

    }

    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == "")
        {
            currentChat = chatField.text;
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }
    
    public void SubmitPrivateOnclick()
    {
        if (privateReceiver != "")
        {
            currentChat = chatField.text;
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    //public void TypeChatOnValueChange(string valueIn)
    //{
    //    currentChat = valueIn;
    //}

    public void RecerverOnValueChange( string ValueIn)
    {
        privateReceiver = ValueIn;
    }

    public void DebugReturn(DebugLevel level, string message)
    {
       
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
        isConnected = true;
        //joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel"});
    }

    public void OnDisconnected()
    {
       // throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisPlay.text += "\n " + msgs;

            Debug.Log(senders[i]);
            Debug.Log(msgs);

        }


    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";
        msgs = string.Format("(Private) {0}: {1}", sender, message );
        chatDisPlay.text += "\n " + msgs;

        Debug.Log(msgs);
       
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
       
    }

    public void OnUserSubscribed(string channel, string user)
    {
       
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    IEnumerator WaitingtoSetPlayerName()
    {
        yield return new WaitForSeconds(1f);
        ChatConnectOnClick();
    }

    private void Start()
    {
        StartCoroutine(WaitingtoSetPlayerName());
    }

    private void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if (chatField .text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateOnclick();
        } 
    }
}
