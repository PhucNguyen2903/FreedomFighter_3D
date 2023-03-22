using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZoombieCreatingDemo : ZombieManager
{
    public GameObject prefab;
    public PhotonView PV;
    public List<GameObject> zombieList1 = new List<GameObject>();
    public List<GameObject> zombieList2 = new List<GameObject>();
    public List<GameObject> zombieList3 = new List<GameObject>();
    public List<GameObject> zombieList4 = new List<GameObject>();
    



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
        
        for (int i = 0; i < zombieList1.Count; i++)
        {
            GameObject zombieObj = Instantiate(zombieList1[i], CaveFirst.position, CaveFirst.rotation);
            zombieObj.SetActive(true);
        }
       
    }


    [PunRPC]
    void CreatingZombieTurn2()
    {
        for (int i = 0; i < zombieList2.Count; i++)
        {
            GameObject zombieObj = Instantiate(zombieList2[i], CaveFirst.position, CaveFirst.rotation);
            zombieObj.SetActive(true);
        }

    }

    [PunRPC]
    void CreatingZombieTurn3()
    {
        for (int i = 0; i < zombieList3.Count; i++)
        {
            GameObject zombieObj = Instantiate(zombieList3[i], CaveFirst.position, CaveFirst.rotation);
            zombieObj.SetActive(true);
        }

    }

    [PunRPC]
    void CreatingZombieTurn4()
    {
        for (int i = 0; i < zombieList4.Count; i++)
        {
            GameObject zombieObj = Instantiate(zombieList4[i], CaveFirst.position, CaveFirst.rotation);
            zombieObj.SetActive(true);
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
            PV.RPC("CreatingZombieTurn2", RpcTarget.All);
            turnComplete = 1;
        }
        else if (turn == 3 && turnComplete == 1)
        {
            PV.RPC("CreatingZombieTurn3", RpcTarget.All);
            turnComplete = 2;
        }
        else if (turn == 4 && turnComplete == 2)
        {
            PV.RPC("CreatingZombieTurn4", RpcTarget.All);
            turnComplete = 3;
        }
        else if (turnComplete == 3)
        {
            return;
        }
    }


}
