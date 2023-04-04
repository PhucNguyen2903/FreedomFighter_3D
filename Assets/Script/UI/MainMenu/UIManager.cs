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
    public PhotonChatManager chatManager;
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
        if (PhotonNetwork.InLobby)
        {
            ObserverCallBack("FirstMenu");
        }
        else
        {
            ObserverCallBack("RoomPopup");
            PhotonRoom.instance.ReturnFromGamePlayListPlayerUpdata();
            PhotonRoom.instance.UpdatepPlayerListInFirstMenu();
            chatManager.SetUserName(PhotonNetwork.LocalPlayer.NickName);
        }
        PlayerInfo.Instance.RemoveAllItemEquip();
        List<ItemShop> list = StorageManager.Instance.TakeEquipItemList();
        List<ItemShop> storageList = StorageManager.Instance.TakeStorageItemList();
        PlayerInfo.Instance.SetITem(list,storageList);
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
        //ObserverCallBack("FirstMenu");
        OnclickBack();
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }



}
