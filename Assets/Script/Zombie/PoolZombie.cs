using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolZombie : MonoBehaviour
{
    private static PoolZombie instance;
    public static PoolZombie Instance => instance;
    public Transform PoolDie;
    public List<GameObject> PoolofZombieD = new List<GameObject>();
    public List<GameObject> PoolofZombieReuse = new List<GameObject>();


    void Start()
    {
        PoolZombie.instance = this;
    }
    protected virtual void Reset()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPool(string name)
    {
        GameObject obj = GetPoolByName(name);
        if (obj != null)
        {
            RemoveInPool(obj);
            return obj;
        }
        return null;
      
    }
    
    public GameObject GetPoolByName(string name)
    {
        foreach (GameObject item in this.PoolofZombieD)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }

    public void RemoveInPool(GameObject gameObj)
    {
        if (this.PoolofZombieD.Contains(gameObj))
        {
            GetOutFromPool(gameObj);
            this.PoolofZombieD.Remove(gameObj);
        }
    }

    public void GetOutFromPool(GameObject gameObj)
    {
        this.PoolofZombieReuse.Add(gameObj);
    }

    public void ReturnPool(GameObject gameObj)
    {
        this.PoolofZombieD.Add(gameObj);
        this.PoolofZombieReuse.Remove(gameObj);
    }
    
}
