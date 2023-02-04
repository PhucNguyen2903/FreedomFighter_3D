using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomZombieAttack : ZombieAttack
{
    [Header("Boom Zombie Attack")]
    [SerializeField] ParticleSystem boomingPrefab;
    public float explosionRadius;
   

    
    public override void OnAttack(int index)
    {
        
        Instantiate(boomingPrefab,transform.position,transform.rotation);
        Destroy(gameObject);
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
        PlayerHealth health = victim.GetComponentInParent<PlayerHealth>();
        StartCoroutine(Wait());
        if (health != null )
        {
            Player.Instance.PlayerHealth.TakeDamage(damage);
          
        }
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.85f);
    }

    

}
