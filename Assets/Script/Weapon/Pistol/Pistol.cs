using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Pistol : Shooting
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected UnityEvent onFire;
    [SerializeField] protected AudioSource fireSound;
    private float lastShot;
    private float interval;
    public int rpm;
    public GunAmmo gunAmmo;
    [SerializeField] PhotonView PV;

    private void Start()
    {
        interval = 60f / rpm;
    }
    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetMouseButtonDown(0))
        //{
        //    updateFiring();
        //}

    }

    public void PistolButton()
    {

        if (Input.GetMouseButtonDown(0))
        {
            updateFiring();
        }
    }
    private void Fire()
    {
        bool isShooting = gunAmmo.isShooting();
        if (!isShooting) return;
        //anim.Play("Fire", layer: -1, normalizedTime: 0);
        PV.RPC("CallAnimFire", RpcTarget.All);
        fireSound.Play();
        onFire.Invoke();
    }

    private void updateFiring()
    {
        if (Time.time - lastShot >= interval)
        {
            Fire();
            lastShot = Time.time;
        }
    }

    [PunRPC]
    public void CallAnimFire()
    {
        anim.Play("Fire", layer: -1, normalizedTime: 0);
    }
}
