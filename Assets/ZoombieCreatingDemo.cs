using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZoombieCreatingDemo : ZombieManager
{

    private static ZoombieCreatingDemo instance;
    public static ZoombieCreatingDemo Instance => instance;
    public GameObject prefab;
    public Transform PoolLive;
    public PhotonView PV;
    public PoolZombie poolZombie;
    public List<GameObject> zombieList1 = new List<GameObject>();





    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        if (PV.IsMine)
        {
            StartCoroutine(WaitReady());
            ZombieController.Instance.enemySpawner.Startgame();
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
        if (zombieObj == null) return;
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
        CreatingTurnZombie("ZombieNormal", 5);
        //CreatingTurnZombie("ZombieBoss", 1);
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
        else if (turn == 5 && turnComplete == 3)
        {
            CreatingTurnZombie("ZombieNormal", 5);
            CreatingTurnZombie("ZombieBoss", 4);
            StartCoroutine(CreatingZombieAfterBoss());
            turnComplete = 4;
        }
        else if (turn == 6 && turnComplete == 4)
        {
            PlayerWin();
            turnComplete = 5;
        }
        else if (turnComplete == 5)
        {
            return;
        }

    }

    IEnumerator CreatingZombieAfterBoss()
    {
        yield return new WaitForSeconds(5f);
        CreatingTurnZombie("ZombieNormal", 5);
        CreatingTurnZombie("ZombieAdvance", 5);
        CreatingTurnZombie("ZombieBoom", 5);
        CreatingTurnZombie("ZombieGhoul", 5);
    }


    public GameObject TakeZombieByName(string name)
    {
        GameObject poolInpool = poolZombie.GetPool(name);
        if (poolInpool != null) return poolInpool;
        return null;
    }






}
