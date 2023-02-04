using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAmmo : GunAmmo
{

    [SerializeField] private AudioSource reloadSound;
    public override void Reload()
    {
        anim.Play("Reload", layer: -1, normalizedTime: 0);
        reloadSound.Play();
        AddAmmo();
    }

    public override void SingleFireAmmoCounter()
    {
        base.SingleFireAmmoCounter();
    }

    public override void AddAmmo()
    {
        base.AddAmmo();
    }



}
