using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSpawner : Spawner
{
    private static DropItemSpawner instance;
    public static DropItemSpawner Instance => instance;
    //public string healthBox = "HealthBox";
    public string newHealthBox = "NewHealthBox";
    public string bullet = "BulletBox";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


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
