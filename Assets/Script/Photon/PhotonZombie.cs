using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PhotonZombie : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject zombie;
    GameObject zombieController;

    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        CreateZombie();
    }


    private void CreateZombie()
    {
        Debug.Log("Creating zombie");
        //zombieController = PhotonNetwork.Instantiate(Path.Combine("ZombiePrefab", "ZombieNormal"), zombie.transform.position, zombie.transform.rotation, 0, new object[] { PV.ViewID });
        //PhotonNetwork.Instantiate(Path.Combine("ZombiePregfab", "ZombieNormal"), playerTransform.position, playerTransform.rotation);
        StartCoroutine(WaitingAfterPlayerIn());
    }

    public void Die()
    {
        PhotonNetwork.Destroy(zombieController);
        //CreateZombie();
    }
    IEnumerator WaitingAfterPlayerIn()
    {
        yield return new WaitForSeconds(3f);
        zombieController = PhotonNetwork.Instantiate(Path.Combine("ZombiePrefab", "ZombieNormal"), zombie.transform.position, zombie.transform.rotation, 0, new object[] { PV.ViewID });
    }

}
