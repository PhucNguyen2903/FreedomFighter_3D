using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolZombie : MonoBehaviour
{
    private static PoolZombie instance;
    public static PoolZombie Instance => instance;
    public Transform PoolDie;
    public List<GameObject> PoolofZombieD = new List<GameObject>();
    public List<GameObject> PoolofZombieReuse;


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
    public GameObject GetReuseByName(string name)
    {
        Debug.Log(name + "inGetReuse");
        foreach (GameObject item in PoolofZombieReuse)
        {
            Debug.Log(item.name);
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
    public void GetInPool(GameObject gameObj)
    {
        Debug.Log("InPoolllllllllllllllllllllllllllllllllllll");
        this.PoolofZombieD.Add(gameObj);
    }


    public void ReturnPool(string name)
    {
        Debug.Log("NameZombie" + name + " :xx");
        GameObject obj = GetReuseByName(name);
        if (obj != null)
        {
            Debug.Log("NameZombiedifNull" + obj);
            RemoveInReuse(obj);
        }
    }
    public void RemoveInReuse(GameObject gameObj)
    {
        if (this.PoolofZombieReuse.Contains(gameObj))
        {
            GetInPool(gameObj);
            this.PoolofZombieReuse.Remove(gameObj);
        }
    }

}
