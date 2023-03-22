using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instnace => instance;
   
    public List<Observer> observerList = new List<Observer>();
    public GameObject Backbutton;
    private void Awake()
    {
        UIManager.instance = this;
    }
    private void  Start()
    {
        StartCoroutine(Waiting());
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }




   

    public void OnclickBack()
    {
        if (PhotonNetwork.InRoom)
        {
            ObserverCallBack("RoomPopup");
        }
        else
        {
            ObserverCallBack("FirstMenu");
        }
        PlayerInfo.Instance.RemoveAllItemEquip();
        List<ItemShop> list = StorageManager.Instance.TakeEquipItemList();
        PlayerInfo.Instance.SetITem(list);
    }

    public void ObserverCallBack(string name)
    {
        foreach (Observer obser in this.observerList)
        {
            obser.SecActive(obser.name == name);
        }
    }
    
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.01f);
        ObserverCallBack("FirstMenu");
    }


}
