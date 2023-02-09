using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutomaticShooting : Shooting
{
    public Animator anim;
    public int rpm;
  

    public AudioSource shootSound;

    public UnityEvent onShoot;

    private float lastShot;
    private float interval;

    // Start is called before the first frame update
    void Start()
    {
        interval = 60f / rpm;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateFiring();
        }
    }

    private void UpdateFiring()
    {
        if (Time.time - lastShot >= interval)
        {
            Shoot();
            lastShot = Time.time;
        }
    }

    private void Shoot()
    {
        anim.Play("Shoot", layer: -1, normalizedTime:0 );
        shootSound.Play();
        onShoot.Invoke();
    }

    
}
