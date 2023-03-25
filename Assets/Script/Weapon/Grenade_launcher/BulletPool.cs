using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //public List<GameObject> BulletPoolList = new List<GameObject>();
    //public List<GameObject> BulletReuseList;


    //public GameObject GetPool(string name)
    //{
    //    GameObject obj = GetPoolByName(name);
    //    if (obj != null)
    //    {
    //        RemoveInPool(obj);
    //        return obj;
    //    }
    //    return null;

    //}
    //public GameObject GetPoolByName(string name)
    //{
    //    foreach (GameObject item in this.BulletPoolList)
    //    {
    //        if (item.name == name)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}
    //public void RemoveInPool(GameObject gameObj)
    //{
    //    if (this.BulletPoolList.Contains(gameObj))
    //    {
    //        GetOutFromPool(gameObj);
    //        this.BulletPoolList.Remove(gameObj);
    //    }
    //}
    //public void GetOutFromPool(GameObject gameObj)
    //{
    //    this.BulletReuseList.Add(gameObj);
    //}
    //public void GetInPool(GameObject gameObj)
    //{
    //    Debug.Log("InPoolllllllllllllllllllllllllllllllllllllet");
    //    this.BulletPoolList.Add(gameObj);
    //}
    //public void ReturnPool(string name)
    //{
    //    Debug.Log("NameZombie" + name + " :xx");
    //    GameObject obj = GetReuseByName(name);
    //    if (obj != null)
    //    {
    //        Debug.Log("NameZombiedifNull" + obj);
    //        RemoveInReuse(obj);
    //    }
    //}
    //public GameObject GetReuseByName(string name)
    //{
    //    Debug.Log(name + "inGetReuse");
    //    foreach (GameObject item in BulletReuseList)
    //    {
    //        Debug.Log(item.name);
    //        if (item.name == name)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}
    //public void RemoveInReuse(GameObject gameObj)
    //{
    //    if (this.BulletReuseList.Contains(gameObj))
    //    {
    //        GetInPool(gameObj);
    //        this.BulletReuseList.Remove(gameObj);
    //    }
    //}
}
