using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Photon.Pun;

public class ZombieMovement : MonoBehaviour
{
           Transform PlayerFoot;
    public Animator anim;
    public NavMeshAgent agent;
    public float reachingRadius;
    public Health health;
    public UnityEvent onDesternationReached;
    public UnityEvent onStartMoving;
    public float explosionRadius = 200;
    Transform Player_Foot;




    private bool _isMovingValue;

    public bool IsMoving
    {
        get => _isMovingValue;

        private set
        {
            if (_isMovingValue == value) return;
            {
                _isMovingValue = value;
                OnIsMovingValueChanged();
            }
        }
    }
    private void Awake()
    {
        FindObject();
    }


    private void Update()
    {
        if (Player_Foot == null) return;
        float distance = Vector3.Distance(transform.position, Player_Foot.position);
            IsMoving = distance > reachingRadius;


        if (IsMoving)
        {
            agent.isStopped = false;
            agent.SetDestination(Player_Foot.position);
            anim.SetBool("isWalking", true);
        }
        else 
        {
            agent.isStopped = true;
            anim.SetBool("isWalking", false);
        }

        if (health.HealthPoint <= 0)
        {
            agent.isStopped = true;
        }



        //if (distance > reachingRadius)
        //{
        //    agent.isStopped = false;
        //    agent.SetDestination(PlayerFoot.position);
        //    anim.SetBool("isWalking", true);
        //}
        //else
        //{
        //    agent.isStopped = true;
        //    anim.SetBool("isWalking", false);
        //}
    }
    private void OnIsMovingValueChanged()
    {
        agent.isStopped = !_isMovingValue;
        anim.SetBool("isWalking", _isMovingValue);
        if (_isMovingValue)
        {
            onStartMoving.Invoke();
        }
        else
        {
            onDesternationReached.Invoke();
        }
    }

    public void FindObject()
    {

        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            PhotonView playerPhotonView = affectedObjects[i].gameObject.GetComponent<PhotonView>();
            PlayerSingleton Player = affectedObjects[i].gameObject.GetComponent<PlayerSingleton>();
            if (playerPhotonView != null && Player != null)
            {
                //Player_Foot = affectedObjects[i].gameObject.GetComponent<Player>().PlayerFoot;
                 Player_Foot = Player.PlayerFoot;
            }

        }
    }
}
