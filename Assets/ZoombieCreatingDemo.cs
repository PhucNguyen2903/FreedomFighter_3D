using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZoombieCreatingDemo : ZombieManager
{
    public GameObject prefab;
    public Transform PoolLive;
    public PhotonView PV;
    public PoolZombie poolZombie;
    public List<GameObject> zombieList1 = new List<GameObject>();
   

   


    private void Awake()
    {
        if (PV.IsMine)
        {
            StartCoroutine(WaitReady());
        }

    }
    private void Update()
    {
        if (PV.IsMine)
        {
            CreatingZombie();
        }
    }


    [PunRPC]
    void CreatingZombie1()
    {

        for (int i = 0; i < 5; i++)
        {
            GameObject zombieObj = TakeZombieByName("ZombieNormal");
            Debug.Log(zombieObj.name + i);
            zombieObj.transform.SetPositionAndRotation(CaveFirst.position, CaveFirst.rotation);
            zombieObj.SetActive(true);
            zombieObj.transform.SetParent(this.PoolLive);

        }

    }


    [PunRPC]
    void CreatingZombieTurn2()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject zombieObj = TakeZombieByName("ZombieAdvance");
            Debug.Log(zombieObj.name + i);
            zombieObj.SetActive(true);
            zombieObj.transform.SetPositionAndRotation(CaveFirst.position, CaveFirst.rotation);
            zombieObj.transform.SetParent(this.PoolLive);
        }
    }

    [PunRPC]
    void CreatingZombieTurn3()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject zombieObj = TakeZombieByName("ZombieBoom");
            zombieObj.SetActive(true);
            zombieObj.transform.SetPositionAndRotation(CaveFirst.position, CaveFirst.rotation);
            zombieObj.transform.SetParent(this.PoolLive);
        }

    }

    [PunRPC]
    void CreatingZombieTurn4()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject zombieObj = TakeZombieByName("ZombieGhoul");
            zombieObj.SetActive(true);
            zombieObj.transform.SetPositionAndRotation(CaveFirst.position, CaveFirst.rotation);
            zombieObj.transform.SetParent(this.PoolLive);
        }
    }


    IEnumerator WaitReady()
    {
        yield return new WaitForSeconds(10f);
        PV.RPC("CreatingZombie1", RpcTarget.All);
    }

    public override void CreatingZombie()
    {
        CheckTurn();

        if (turn == 2 && turnComplete == 0)
        {
            PV.RPC("CreatingZombie1", RpcTarget.All);
            PV.RPC("CreatingZombieTurn2", RpcTarget.All);
            turnComplete = 1;
        }
        else if (turn == 3 && turnComplete == 1)
        {
            PV.RPC("CreatingZombie1", RpcTarget.All);
            PV.RPC("CreatingZombieTurn2", RpcTarget.All);
            PV.RPC("CreatingZombieTurn3", RpcTarget.All);
            turnComplete = 2;
        }
        else if (turn == 4 && turnComplete == 2)
        {
            PV.RPC("CreatingZombie1", RpcTarget.All);
            PV.RPC("CreatingZombieTurn2", RpcTarget.All);
            PV.RPC("CreatingZombieTurn3", RpcTarget.All);
            PV.RPC("CreatingZombieTurn4", RpcTarget.All);
            turnComplete = 3;
        }
        else if (turnComplete == 3)
        {
            return;
        }
    }


    public GameObject TakeZombieByName(string name)
    {
        GameObject poolInpool = poolZombie.GetPool(name);
        if (poolInpool != null) return poolInpool;
        return null;
    }


   


}
