using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public Animator anim;
    public int damage;
    //public PlayerHealth playerHealth;

    public virtual void StartAttack() => anim.SetBool("isAttacking", true);
    public virtual void StopAttack() => anim.SetBool("isAttacking", false);

    public virtual void OnAttack(int index)
    {
        Player.Instance.PlayerHealth.TakeDamage(damage);
        //PlayerHealth.Instance.TakeDamage(damage);
        //playerHealth.TakeDamage(damage);
        if (index == 1)
        {
            Player.Instance.playerUi.ShowLeftScratch();
        }
        else
        {
            Player.Instance.playerUi.ShowRightScratch();
        }
    }
}
