using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using Photon.Pun;

public class GameSetUpController : MonoBehaviour
{
    public Transform playerTransform;
    private static GameSetUpController instance;

    public static GameSetUpController Instance => instance;
    private void Start()
    {
        GameSetUpController.instance = this;
        CreatePlayerManager();

    }

    

    private void CreatePlayerManager()
    {
        Debug.Log("Creating PlayerManager");
        PhotonNetwork.Instantiate(Path.Combine("PlayerPrefab", "PlayerManager"), Vector3.zero, Quaternion.identity);
    }

}
