using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManagerInGame : MonoBehaviour
{
    private Transform CreatingPlayerTransform;
    public PhotonView photonView;

    private void Start()
    {
        CreatingPlayerTransform = GameSetUpController.Instance.playerTransform;
        if (photonView.IsMine)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        GameObject playerObj = PhotonNetwork.Instantiate(Path.Combine("PlayerPrefab", "Player"), CreatingPlayerTransform.position, CreatingPlayerTransform.rotation);
    }
}
