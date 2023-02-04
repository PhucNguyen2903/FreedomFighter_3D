using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDespawn : DeSpawn
{


    protected override bool CanDespawn()
    {
        if (OnDieZombie()) return true;
        return false;

    }
    protected override void DespawnObject()
    {

    }

    private bool OnDieZombie()
    {
        //return Health.Instance.death;
        return true;
    }
}
