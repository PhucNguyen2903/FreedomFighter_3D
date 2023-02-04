using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ZombieMovement : MonoBehaviour
{
           Transform PlayerFoot;
    public Animator anim;
    public NavMeshAgent agent;
    public float reachingRadius;
    public Health health;
    public UnityEvent onDesternationReached;
    public UnityEvent onStartMoving;

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
         PlayerFoot = Player.Instance.PlayerFoot;   
    }


    private void Update()
    {
        float distance = Vector3.Distance(transform.position, PlayerFoot.position);
            IsMoving = distance > reachingRadius;


        if (IsMoving)
        {
            agent.isStopped = false;
            agent.SetDestination(PlayerFoot.position);
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
}
