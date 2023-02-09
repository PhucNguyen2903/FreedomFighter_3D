using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAmmoPistol : GunAmmo
{
    
    public override void Reload()
    {
        if (!CanReload()) return;   

        SetLoadAmmo();
        anim.Play("Reload", layer: -1, normalizedTime: 0);
        PlayMusic();
        AddAmmo();

    }

   public override void AddAmmo()
    {
        base.AddAmmo();
     
    }

    
    private void PlayMusic()
    {
        reloadSounds[0].Play();
        reloadSounds[1].Play();
        reloadSounds[2].Play();   
    }
    //IEnumerator  WaitingUntilChange()
    //{
    //    yield return new WaitForSeconds(10f);
    //    AddAmmo();
    //}
}
