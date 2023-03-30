using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GrenadeBullet : MonoBehaviour
{
    public PhotonView PV;
    public GameObject explosionPrefab;
    public float explosionRadius;
    public float explosionForce;
    public int damage;
    public AudioSource BoomingAudio;
    public ScoreText scoreText;

    private List<Health> oldVictims = new List<Health>();
    private void Awake()
    {
       
    }
    public void OnCollisionEnter(Collision collision)
    {
        //Instantiate(explosionPrefab, transform.position, transform.rotation);
        BoomingAudio.Play();
        PV.RPC("RPC_Prefab", RpcTarget.All);
        StartCoroutine(waiytoDestroy());
        //Destroy(gameObject);
        BlowObject();
    }


    [PunRPC]
    public void RPC_Prefab()
    {
        BoomingAudio.Play();
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    IEnumerator waiytoDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    private void BlowObject()
    {
        oldVictims.Clear();
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            DeliverDamage(affectedObjects[i]);
            // AddForceToObject(affectedObjects[i]); 
        }
    }

    private void DeliverDamage(Collider victim)
    {
        Health health = victim.GetComponentInParent<Health>();
        PhotonView PVzombieTakedame = victim.GetComponentInParent<PhotonView>();

        if (health != null && !oldVictims.Contains(health) && PVzombieTakedame != null)
        {
            if (health.HealthPoint < 1) return;
            
            PVzombieTakedame.RPC("TakeDamage", RpcTarget.All, damage);
            scoreText.Score += damage;
            //health.TakeDamage(damage);
            oldVictims.Add(health);
        }
    }

    private void AddForceToObject(Collider affectedObject)
    {
        Rigidbody rigibody = affectedObject.attachedRigidbody;
        if (rigibody)
        {
            rigibody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1, ForceMode.Impulse);
        }
    }

}
