using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBossRightHand : MonoBehaviour
{
    public BossZombieAttack bossZoombieAttack;

    private void OnCollisionEnter(Collision collision)
    {
        var PlayerHealth = collision.collider.GetComponent<PlayerHealth>();
        if (PlayerHealth != null)
        {
            bossZoombieAttack.OnAttack2(PlayerHealth);
        }
    }
}
