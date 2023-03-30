using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieManager : MonoBehaviour
{
    protected string zombieNormal = "ZombieNormal";
    protected string zombieAdvance = "ZombieAdvance";
    protected string zombieBoom = "ZombieBoom";
    protected string zombieGhoul = "ZombieGhoul";
    [SerializeField] public Transform CaveFirst;
    public int numOfDeath;
    public int turn = 1;
    public int turnComplete;



    private void Update()
    {
        //CreatingZombie();
    }
    private void Awake()
    {

        //FirstTurn();
        //ThirdTurn();
        //FourTurn();

    }

    public virtual void CreatingZombie()
    {
        CheckTurn();

        if (turn == 2 && turnComplete == 0)
        {
            FirstTurn();
            SecondTurn();
            turnComplete = 1;
        }
        else if (turn == 3 && turnComplete == 1)
        {
            FirstTurn();
            SecondTurn();
            ThirdTurn();
            turnComplete = 2;
        }
        else if (turn == 4 && turnComplete == 2)
        {
            FirstTurn();
            SecondTurn();
            ThirdTurn();
            FourTurn();
            turnComplete = 3;
        }



    }


    [PunRPC]
    private void FirstTurn()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform newZombie = ZombieController.Instance.enemySpawner.Spawn(zombieNormal, CaveFirst.position, CaveFirst.rotation);

            newZombie.gameObject.SetActive(true);
        }

    }
    private void SecondTurn()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform newZombie = ZombieController.Instance.enemySpawner.Spawn(zombieAdvance, CaveFirst.position, CaveFirst.rotation);
            newZombie.gameObject.SetActive(true);
        }
    }
    private void ThirdTurn()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform newZombie = ZombieController.Instance.enemySpawner.Spawn(zombieBoom, CaveFirst.position, CaveFirst.rotation);
            newZombie.gameObject.SetActive(true);
        }
    }

    private void FourTurn()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform newZombie = ZombieController.Instance.enemySpawner.Spawn(zombieGhoul, CaveFirst.position, CaveFirst.rotation);
            newZombie.gameObject.SetActive(true);
        }
    }


    public virtual void CheckTurn()
    {
        numOfDeath = ZombieController.Instance.enemySpawner.count;

        if (numOfDeath > 4 && turnComplete == 0)
        {
            turn = 2;
        }
        else if (numOfDeath > 14 && turnComplete == 1)
        {
            turn = 3;
        }
        else if (numOfDeath > 29 && turnComplete == 2)
        {
            turn = 4;
        }
        else if (numOfDeath > 49 && turnComplete == 3)
        {
            Debug.Log("Turn555555555555");
            turn = 5;
        }
   
    }

    public void PlayerWin()
    {
        PlayerInPlay.Instance.CallPopupGameover();
    }







}
