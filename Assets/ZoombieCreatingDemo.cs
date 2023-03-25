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
    void CreatingZombie(string name)
    {
        GameObject zombieObj = TakeZombieByName(name);
        zombieObj.SetActive(true);
        Health healthZombie = zombieObj.GetComponent<Health>();
        healthZombie.zombieName = name;
        zombieObj.transform.SetPositionAndRotation(CaveFirst.position, CaveFirst.rotation);
        zombieObj.transform.SetParent(this.PoolLive);
    }

    void CreatingTurnZombie(string name, int nums)
    {
        for (int i = 0; i < nums; i++)
        {
            PV.RPC("CreatingZombie", RpcTarget.All, name);
        }

    }

    IEnumerator WaitReady()
    {
        yield return new WaitForSeconds(10f);
        CreatingTurnZombie("ZombieNormal",5);
    }

    public override void CreatingZombie()
    {
        CheckTurn();

        if (turn == 2 && turnComplete == 0)
        {
            CreatingTurnZombie("ZombieNormal", 5);
            CreatingTurnZombie("ZombieAdvance", 5);
            turnComplete = 1;
        }
        else if (turn == 3 && turnComplete == 1)
        {
            CreatingTurnZombie("ZombieNormal", 5);
            CreatingTurnZombie("ZombieAdvance", 5);
            CreatingTurnZombie("ZombieBoom", 5);
            turnComplete = 2;
        }
        else if (turn == 4 && turnComplete == 2)
        {
            CreatingTurnZombie("ZombieNormal", 5);
            CreatingTurnZombie("ZombieAdvance", 5);
            CreatingTurnZombie("ZombieBoom", 5);
            CreatingTurnZombie("ZombieGhoul", 5);
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
