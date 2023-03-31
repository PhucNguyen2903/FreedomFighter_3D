using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerUIManager : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public GameObject player;
    public GameObject PopupGameOver;
    public ScoreText scoreText;
    public TimeCount timeCount;
    public string Name;
    public int rank;
    int timeminus;
    int timeSecond;

    private void Awake()
    {
    }
    private void Start()
    {

    }

    public void OnClickHome()
    {
        if (PV.IsMine)
        {
            HomeOnclick();
        }
    }

    void HomeOnclick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.LoadLevel("Lobby");
            PhotonNetwork.AutomaticallySyncScene = true;

        }
        else
        {
            PhotonNetwork.LoadLevel("Lobby");
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        PhotonNetwork.Destroy(this.player);
        //PV.RPC(nameof(DestroyPlayer), RpcTarget.All);
        //player.SetActive(false);
    }

    public void OnclickQuit()
    {
        if (!PV.IsMine) return;
        Application.Quit();
    }

    [PunRPC]
    void LoadLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    [PunRPC]
    void NoticeName()
    {
        Name = PhotonNetwork.LocalPlayer.NickName;
    }

    public string TakeName()
    {
        PV.RPC("NoticeName", RpcTarget.All);
        return Name;
    }

    public int TakeMinus()
    {
        PV.RPC("NoticeTimeMinus", RpcTarget.All);
        return timeminus;

    }

    [PunRPC]
    void NoticeTimeMinus()
    {
        timeminus = timeCount.calTimeMinus;
    }
    public int TakeSecond()
    {
        PV.RPC("NoticeSecond", RpcTarget.All);
        return timeSecond;

    }

    [PunRPC]
    void NoticeSecond()
    {
        timeSecond = timeCount.calTimeSecond;
    }
    [PunRPC]
    void DestroyPlayer()
    {
        Destroy(this.player);
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

}
