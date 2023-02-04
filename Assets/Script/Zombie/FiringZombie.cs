using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FiringZombie : MonoBehaviour
{
    [SerializeField] private Transform attackRadius;
    [SerializeField] private Transform attackRadiusEnd;
    private Vector3 attackRadiusVector;
    private Vector3 attackRadiusEndVector;
    public int damage;
   
    [SerializeField] private ParticleSystem isfiredZombie;


    private void Awake()
    {
        
    }

    private List<Health> oldVictims = new List<Health>();


    private void Update()
    {
         attackRadiusVector = new Vector3(attackRadius.transform.position.x, attackRadius.transform.position.y, attackRadius.transform.position.z);
         attackRadiusEndVector = new Vector3(attackRadiusEnd.transform.position.x, attackRadiusEnd.transform.position.y, attackRadiusEnd.transform.position.z);
    }
    [SerializeField]public void BlowObject()
    {
        oldVictims.Clear();
        Collider[] affectedObjects = Physics.OverlapCapsule(attackRadiusVector, attackRadiusEndVector,1);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            DeliverDamage(affectedObjects[i]);
            // AddForceToObject(affectedObjects[i]); 
        }
    }
    private void DeliverDamage(Collider victim)
    {
        Health health = victim.GetComponentInParent<Health>();
        if (health != null && !oldVictims.Contains(health))
        {
            
            effectFiredZombie(victim);
            health.TakeDamage(damage);
            oldVictims.Add(health);
        }
       
    }

    private void effectFiredZombie(Collider victim)
    {
        Instantiate(isfiredZombie,victim.transform.position, victim.transform.rotation);
    }

   
}
