using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemController : MonoBehaviour
{
     public ItemShop itemShop;
  







    public void AddItem(ItemShop itemShopAdd)
    {
        itemShop = itemShopAdd;

    }

    public virtual void Buy()
    {
        int playerGold = PlayerInfo.Instance.gold;
        if (playerGold < itemShop.price) return;
        PlayerInfo.Instance.DevideGold(itemShop.price);
        StorageManager.Instance.AddStorage(itemShop);
      //  ShopManager.Instance.SetupPlayerInfo();
       
    }

  
}
