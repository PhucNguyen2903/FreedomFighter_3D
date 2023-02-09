using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FLamethrower : Shooting
{
    [SerializeField] private GameObject fireWave;
    [SerializeField] private GameObject attackRadius;
    [SerializeField] private GameObject firePos;
    public Animator anim;
    [SerializeField] public UnityEvent onfire;
    [SerializeField] protected AudioSource fireSound;
  

   
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
            OnFire();
            
        }
        else
        {
            UnFire();   
        }
    }

    private void Fire()
    {
        anim.Play("Fire", layer: -1, normalizedTime: 0);
        Instantiate(fireWave,firePos.transform.position, firePos.transform.rotation);
        onfire.Invoke();
    }

    private void OnFire()
    {
        fireSound.Play();
        
    }

    private void UnFire()
    {
        fireSound.Stop();
    }
    
   
}
