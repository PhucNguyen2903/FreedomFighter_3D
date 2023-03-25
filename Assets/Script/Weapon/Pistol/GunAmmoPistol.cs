using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunAmmoPistol : GunAmmo
{      
    public override void Reload()
    {
        if (!PV.IsMine) return;
        if (!CanReload()) return;   
       // SetLoadAmmo();
        PV.RPC("CallAnim", RpcTarget.All);
        PlayMusic();
        AddAmmo();

    }    

    private void PlayMusic()
    {
        reloadSounds[0].Play();
        reloadSounds[1].Play();
        reloadSounds[2].Play();   
    }

    [PunRPC]
    public void CallAnim()
    {
        anim.Play("Reload", layer: -1, normalizedTime: 0);
    }
}
