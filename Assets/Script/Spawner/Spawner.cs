using System.Collections;
using UnityEngine;
using System.Collections.Generic;




public abstract class Spawner : SaiMono
{
    [SerializeField] public Transform pool;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] public List<Transform> poolObjs;

   [SerializeField] public int count = 0;


    


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;


        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 spawPos, Quaternion spawRos)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found: " + prefabName);
            return null;
        }

        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawPos, spawRos);

        newPrefab.parent = this.pool;
        return newPrefab;
    }

    public virtual Transform GetPrefabByName(string PrefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == PrefabName) return prefab;
        }
        return null;
    }

    public virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj  in this.poolObjs)
        {
            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }

        }
            Transform newPrefab = Instantiate(prefab);
            newPrefab.name = prefab.name;
            return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        //this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        count++;
    }


}
