using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMallItemController : ShopItemController
{
    

    



   public override void Buy()
    {
        int playerPoint = PlayerInfo.Instance.point;
        if (playerPoint < itemShop.price) return;
        PlayerInfo.Instance.DevidePoint(itemShop.price);
        PointMallManager.Instance.SetupPlayerInfo();
        StorageManager.Instance.AddStorage(itemShop);
    }
}
