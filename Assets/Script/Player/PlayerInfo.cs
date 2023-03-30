using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance => instance;
    public List<ItemShop> itemListPlayer = new List<ItemShop>();
    public static List<ItemShop> ListGun = new List<ItemShop>();
    public List<ItemShop> ListStorage = new List<ItemShop>();
    public List<ItemShop> Listadd = new List<ItemShop>();

    Data data;


    public int gold;
    public int point;
    public int Ticket;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            data = new Data();
            data.GunItem = new List<ItemShop>();
            data.storaListData = new List<ItemShop>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        TakeData();
    }

    private void Start()
    {
        Listadd = ListGun;
    }
    private void Update()
    {

    }

    public string TakeList()
    {
        string GunList = JsonUtility.ToJson(PlayerInfo.instance.data);
        return GunList;
    }

    public void TakeData()
    {
        string data = TakeList();
        Data newData = JsonUtility.FromJson<Data>(data);
        gold = newData.gold;
        point = newData.point;
        Ticket = newData.Ticket;
        List<ItemShop> gunList = newData.GunItem;
        List<ItemShop> storaList = newData.storaListData;

        foreach (ItemShop item in gunList)
        {
            if (!ListGun.Contains(item))
            {
                ListGun.Add(item);
            }
            Debug.Log(item.name);
        }
        foreach (ItemShop item in storaList)
        {
            ListStorage.Add(item);
            Debug.Log(item.name);
        }
        Debug.Log("takeData");

    }

    public void DevideGold(int goldDevide)
    {
        gold -= goldDevide;
        data.gold -= goldDevide;


    }

    public void DevidePoint(int pointDevide)
    {
        point -= pointDevide * 2;
        data.point -= pointDevide * 2;
    }

    public void DevideTicket(int ticketDevide)
    {
        Ticket -= ticketDevide;
        data.Ticket -= ticketDevide;
    }

    public void AddPoint(int pointDevide)
    {
        point += pointDevide;
        data.point += pointDevide;
    }


    public void SetITem(List<ItemShop> itemList, List<ItemShop> storageList)
    {
        foreach (ItemShop item in itemList)
        {
            itemListPlayer.Add(item);
            PlayerInfo.instance.data.GunItem.Add(item);
        }
        foreach (ItemShop item in storageList)
        {
            PlayerInfo.instance.data.storaListData.Add(item);
        }

        data.bulletbox = StorageManager.Instance.TakeBulletBox();
        data.healthBox = StorageManager.Instance.TakeHealthBox();
        Debug.Log("setItemmmmmmmmmmmm");
    }

    public void RemoveAllItemEquip()
    {
        itemListPlayer.Clear();
        PlayerInfo.instance.data.GunItem.Clear();
        PlayerInfo.instance.data.storaListData.Clear();
    }

    public void Addnum(ItemShop itemShop)
    {
        if (itemShop.itemType == ItemShop.ItemType.Blood)
        {
            data.healthBox.count++;
        }
        if (itemShop.itemType == ItemShop.ItemType.Bullet)
        {
            data.bulletbox.count++;
        }
        
    }
    public void Denum(ItemShop itemShop)
    {
        if (itemShop.itemType == ItemShop.ItemType.Blood)
        {
            data.healthBox.count--;
        }
        if (itemShop.itemType == ItemShop.ItemType.Bullet)
        {
            data.bulletbox.count--;
        }

    }

}

[Serializable]
public class Data
{
    public List<ItemShop> GunItem;
    public List<ItemShop> storaListData;
    public ItemShop bulletbox;
    public ItemShop healthBox;
    public int gold = 500;
    public int point = 1000;
    public int Ticket = 10;
}

