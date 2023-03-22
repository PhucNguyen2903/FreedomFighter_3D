using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance => instance;
    public List<ItemShop> itemListPlayer = new List<ItemShop>();

    Data data;


    public int gold = 500;
    public int point = 1000;
    public int Ticket = 10;

    private void Awake()
    {
        data = new Data();
        data.GunItem = new List<ItemShop>();
       PlayerInfo.instance = this;
       DontDestroyOnLoad(this.gameObject);
    }

    public string TakeList()
    {
        string GunList = JsonUtility.ToJson(PlayerInfo.instance.data);
        return GunList;
    }

    public void DevideGold(int goldDevide)
    {
        gold -= goldDevide;
    }

    public void DevidePoint(int pointDevide)
    {
        point -= pointDevide*2;
    }

    public void DevideTicket(int ticketDevide)
    {
        Ticket -= ticketDevide;
    }

    public void AddPoint(int pointDevide)
    {
        point += pointDevide;
    }
    public void SetITem(List<ItemShop> itemList)
    {
        foreach (ItemShop item in itemList)
        {
            itemListPlayer.Add(item);
            PlayerInfo.instance.data.GunItem.Add(item);
        }

        data.bulletbox = StorageManager.Instance.TakeBulletBox();
        data.healthBox = StorageManager.Instance.TakeHealthBox();
    }

    public void RemoveAllItemEquip()
    {
        PlayerInfo.instance.data.GunItem.Clear();
    }

}

public class Data
{
   public List<ItemShop> GunItem;
   public ItemShop bulletbox;
    public ItemShop healthBox;
}
