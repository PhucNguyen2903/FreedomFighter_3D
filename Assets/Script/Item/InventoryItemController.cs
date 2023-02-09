using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Items items;
    public Button removeButton;



    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(items);
        Destroy(gameObject);
    }

    public void AddItem( Items newItem)
    {
        items = newItem;
    }

    public void UseItem()
    {
        switch (items.itemType)
        {
            case Items.ItemType.Blood:
                Player.Instance.PlayerHealth.IncreaseHP(items.value);
                break;
            case Items.ItemType.Bullet:
                GunAmmo.Instance.IncreasenNumofMag();
                break;
            case Items.ItemType.Weapons:
                Debug.Log("You have ever set used-Weapon Item");
                break;
            default:
                break;
        }

        RemoveItem();
    }
}
