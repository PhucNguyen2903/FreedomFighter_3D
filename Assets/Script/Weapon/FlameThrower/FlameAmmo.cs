using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlameAmmo : GunAmmo
{


    [SerializeField] private AudioSource reloadSound;
    public override void Reload()
    {
        if (!CanReload()) return;
      //   SetLoadAmmo();
       // anim.Play("Reload", layer: -1, normalizedTime: 0);
        PV.RPC("CallAnimReload", RpcTarget.All);
        reloadSound.Play();
        AddAmmo();
    }

    [PunRPC]
    public void CallAnimReload()
    {
        anim.Play("Reload", layer: -1, normalizedTime: 0);
    }

}
