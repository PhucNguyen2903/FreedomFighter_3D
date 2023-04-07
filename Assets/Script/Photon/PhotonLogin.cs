using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;


public class PhotonLogin : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputUsername;
    private string account = "fighter";
    private string password = "fighter";

    private void Awake()
    {
        this.inputUsername.text = "Fighter" + Random.Range(0,99).ToString();
    }

    public virtual void login()
    {
        //if (!IsLogin()) return;
        string name = inputUsername.text;
        Debug.Log(transform.name + ": Login" + name);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
       

    }

    public bool IsLogin()
    {
        string Inputaccount = inputUsername.text;

        if (Inputaccount == account)
        {
            return true;
        }
        return false;

    }
    public virtual void Logout()
    {
        Debug.Log(transform.name + ": Logout ");
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaste");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel("Lobby");
    }


}
