using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAmmo : GunAmmo
{

    [SerializeField] private AudioSource reloadSound;
    public override void Reload()
    {
        if (!CanReload()) return;

        SetLoadAmmo();
        anim.Play("Reload", layer: -1, normalizedTime: 0);
        reloadSound.Play();
        AddAmmo();
    }

   



}
