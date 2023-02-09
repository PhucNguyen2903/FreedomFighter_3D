using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingGrenade : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject grenade;
    public float bulletSpeed;
    [SerializeField] private Transform armThrowingPos;
    [SerializeField] private GameObject GrenadeInHand;
    [SerializeField] private AudioSource throwSound;

   private void Awake()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            throwing();
        }
    }


    private void throwing()
    {
        anim.Play("Throw", layer: -1, normalizedTime: 0);
        GrenadeInHand.SetActive(false);
        throwingSpawner();
        throwSound.Play();
    }

    private void throwingSpawner()
    {
        GameObject bullet = Instantiate(grenade, armThrowingPos.position, armThrowingPos.rotation); 
        //bullet.transform.LookAt(throwingPos);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
}
