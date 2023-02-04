using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSpawner : Spawner
{
    //public string healthBox = "HealthBox";
    public string newHealthBox = "NewHealthBox";
   

    protected override void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;


        Transform prefabObj = transform.Find("ItemPrefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }

   
}
