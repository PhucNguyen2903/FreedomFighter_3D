using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class FLamethrower : Shooting
{
    [SerializeField] private GameObject fireWave;
    [SerializeField] private GameObject attackRadius;
    [SerializeField] private GameObject firePos;
    public Animator anim;
    [SerializeField] public UnityEvent onfire;
    [SerializeField] protected AudioSource fireSound;
    public PhotonView PV;
    public FlameAmmo flameAmmo;



    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Fire();
        //    OnFire();

        //}
        //else
        //{
        //    UnFire();   
        //}
    }

    public void FlameButton()
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
        bool isShooting = flameAmmo.isShooting();
        if (!isShooting) return;
        // anim.Play("Fire", layer: -1, normalizedTime: 0);
        // Instantiate(fireWave,firePos.transform.position, firePos.transform.rotation);
        PV.RPC("CallAnim", RpcTarget.All);
        PV.RPC("RPC_Prefab", RpcTarget.All);
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

    [PunRPC]
    void RPC_Prefab()
    {
        Instantiate(fireWave, firePos.transform.position, firePos.transform.rotation);
    }

    [PunRPC]
    public void CallAnim()
    {
        anim.Play("Fire", layer: -1, normalizedTime: 0);
    }

}
