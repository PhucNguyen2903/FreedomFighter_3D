using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public Animator anim;
    public int damage;
    //public PlayerHealth playerHealth;
    [SerializeField]public  ZombieMovement zombieMovement;

    public virtual void StartAttack() => anim.SetBool("isAttacking", true);
    public virtual void StopAttack() => anim.SetBool("isAttacking", false);

    public virtual void OnAttack(int index)
    {
        var playerHP = zombieMovement.playerHealth.Hp;
        if(playerHP < 1)
        {
            zombieMovement.Player_Foot = null;
            StopAttack();
            zombieMovement.FindObject();
            Debug.Log("PlayerHpRunnnnnn: "+ playerHP);
            return;

        }

        zombieMovement.playerHealth.TakeDamage(damage);
       // PlayerSingleton.Instance.PlayerHealth.TakeDamage(damage);
        //PlayerHealth.Instance.TakeDamage(damage);
        //playerHealth.TakeDamage(damage);
        if (index == 1)
        {
            PlayerSingleton.Instance.playerUi.ShowLeftScratch();
        }
        else
        {
            PlayerSingleton.Instance.playerUi.ShowRightScratch();
        }
    }
}
