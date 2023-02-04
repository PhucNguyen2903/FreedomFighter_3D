using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    protected string zombieNormal = "ZombieNormal";
    protected string zombieAdvance = "ZombieAdvance";
    protected string zombieBoom = "ZombieBoom";
    protected string zombieGhoul = "ZombieGhoul";
    [SerializeField] private Transform CaveFirst;
    public int numOfDeath;
    private int turn = 1;
    private int turnComplete;



    private void Update()
    {
        CreatingZombie();
    }
    private void Awake()
    {
        
        FirstTurn();
        
    }

    private void CreatingZombie()
    {
        CheckTurn();

        if ( turn == 2 && turnComplete == 0)
        {
            FirstTurn();
            SecondTurn();
            turnComplete = 1;
        }
        else if ( turn == 3 && turnComplete == 1)
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


    private void CheckTurn()
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
            
    }

   
    




}
