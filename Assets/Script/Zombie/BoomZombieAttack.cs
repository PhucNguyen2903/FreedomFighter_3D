using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoomZombieAttack : ZombieAttack
{
    [Header("Boom Zombie Attack")]
    [SerializeField] ParticleSystem boomingPrefab;
    public float explosionRadius;
    [SerializeField] PhotonView PV;
    [SerializeField] GameObject BoomZombieobj;
   

    
    public override void OnAttack(int index)
    {
        PV.RPC("CreatingEffect",RpcTarget.All);
        BlowObject();
       
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
        StartCoroutine(Wait());
        if (playerhealth != null && PVscript != null)
        {
            //PlayerSingleton.Instance.PlayerHealth.TakeDamage(damage);
            playerhealth.TakeDamage(damage);
          
        }
        if (health != null && PVscript != null)
        {
            PVscript.RPC("TakeDamage", RpcTarget.All, damage);


        }
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.85f);
    }

    [PunRPC]
    void CreatingEffect()
    {
        Instantiate(boomingPrefab, transform.position, transform.rotation);
      //  Destroy(this.BoomZombieobj);
    }

    

}
