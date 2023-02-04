using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{


    protected override void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;


        Transform prefabObj = transform.Find("ZombiePrefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }

    public override Transform Spawn(string prefabName, Vector3 spawPos, Quaternion spawRos)
    {

        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }

        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawPos, spawRos);
        Health health = newPrefab.gameObject.GetComponent<Health>();
        health.zombieName = prefabName;
        newPrefab.parent = this.pool;
        return newPrefab;
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        Health health = obj.gameObject.GetComponent<Health>();
        health.HealthPoint = 100;
        health.zombieName = "";
        health.itemSpawnerName = "";
        RagdollSwitcher ragdoll = obj.gameObject.GetComponent<RagdollSwitcher>();
        ragdoll.DisableRagdoll();

    }
}
