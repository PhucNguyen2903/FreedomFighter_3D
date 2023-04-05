using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossZombieAttack : ZombieAttack
{
    public float explosionRadius;
    [SerializeField] ParticleSystem boomingPrefab;
    [SerializeField] PhotonView PV;
    [SerializeField] Health zombieHealth;

    public override void OnAttack(int index)
    {
        var playerHP = zombieMovement.playerHealth.Hp;
        if (playerHP < 1)
        {
            zombieMovement.Player_Foot = null;
            StopAttack();
            zombieMovement.FindObject();
            Debug.Log("PlayerHpRunnnnnn: " + playerHP);
            return;

        }

        BlowObject();
        PV.RPC("CreatingEffect", RpcTarget.All);


    }

    public void OnAttack2( PlayerHealth playerHealth)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage/2);
        }

    }


    private void BlowObject()
    {

        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            DeliverDamage(affectedObjects[i]);
            // AddForceToObject(affectedObjects[i]); 
        }
    }

    private void DeliverDamage(Collider victim)
    {
        PlayerHealth playerhealth = victim.GetComponentInParent<PlayerHealth>();
        Health health = victim.GetComponentInParent<Health>();
        var PVscript = victim.GetComponentInParent<PhotonView>();
        if (playerhealth != null && PVscript != null)
        {
            //PlayerSingleton.Instance.PlayerHealth.TakeDamage(damage);
            playerhealth.TakeDamage(damage);

        }
    }

    [PunRPC]
    void CreatingEffect()
    {
        Instantiate(boomingPrefab, transform.position, transform.rotation);
    }
}
