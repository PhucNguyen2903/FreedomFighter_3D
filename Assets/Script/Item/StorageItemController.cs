using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItemController : ShopItemController
{
    public GameObject storageUIitem;
    public float status = 1;








    public void Sell()
    {
        float value = (float)itemShop.price * itemShop.statusIN;
        PlayerInfo.Instance.AddPoint((int)value);
        StorageManager.Instance.SetupPlayerInfo();
        StorageManager.Instance.RemoveStorage(itemShop);
        if (CheckTypeItem(itemShop) && itemShop.count > 0)
        {
            StorageManager.Instance.ShopUI();
        }
        else
        {
            StorageManager.Instance.ItemCountDevide(itemShop);
            Destroy(storageUIitem);
        }
    }

    public void SetStatus(float setStatus)
    {
        status = setStatus;
    }

    public void OnClickEquip()
    {
        if (CheckTypeItem(itemShop)) return;
        int count = StorageManager.Instance.equipedList.Count;
        if (count > 3) return;
        StorageManager.Instance.Equip(itemShop);
        StorageManager.Instance.EquipUI();
        Destroy(this.storageUIitem);
    }

    public void OnClickRemove()
    {
        StorageManager.Instance.RemoveEquip(itemShop);
        StorageManager.Instance.EquipUI();
        StorageManager.Instance.ShopUI();
        Destroy(this.storageUIitem);
    }

    public void OnclickRepair()
    {
        float itemStatus = itemShop.statusIN;
        if (itemStatus >= 1) return;
        int playerPoint = PlayerInfo.Instance.point;
        float repairPrice = itemStatus * 100;
        if (playerPoint < repairPrice) return;
        PlayerInfo.Instance.DevidePoint((int)repairPrice);
        Debug.Log(repairPrice);
        itemShop.statusIN = 1f;
        StorageManager.Instance.ShopUI();
        StorageManager.Instance.SetupPlayerInfo();
    }

    public bool CheckTypeItem(ItemShop itemAdd)
    {
        if ((itemAdd.itemType == ItemShop.ItemType.Blood || itemAdd.itemType == ItemShop.ItemType.Bullet))
        {
            return true;
        }
        return false;
    }


}
