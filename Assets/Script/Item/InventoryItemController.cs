using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class InventoryItemController : MonoBehaviour
{
    private static InventoryItemController instance;
    public static InventoryItemController Instance => instance;
    public PlayerHealth playerHealth;
    [SerializeField] PlayerHealthBar healthBar;
    public GunAmmo gunAmmo;
    public QuickItems quickitem;
    public PhotonView PV;
    public Items items;
    public Button removeButton;



    private void Awake()
    {
        InventoryItemController.instance = this;
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(items);
        UpdateCountText();

    }

    public void AddItem(Items newItem)
    {
        items = newItem;
    }

    public void SetGunAmmo(GunAmmo gunAmmoAdd)
    {
        gunAmmo = gunAmmoAdd;
    }
    public void SetGunAmmo()
    {
        gunAmmo = null;
    }

    public void UseItem()
    {
        if (items.count < 1) return;
        switch (items.itemType)
        {
            case Items.ItemType.Blood:
                playerHealth.IncreaseHP(items.value);
                healthBar.UpdateHealthValue(playerHealth.Hp,playerHealth.maxHP);
                Debug.Log("use Blood..........");
                break;
            case Items.ItemType.Bullet:
                if (gunAmmo == null) return;
                gunAmmo.IncreasenNumofMag();
                Debug.Log("use Bullet..........");
                break;
            case Items.ItemType.Weapons:
                Debug.Log("You have ever set used-Weapon Item");
                break;
            default:
                break;
        }

        RemoveItem();
    }


    


    public void UpdateCountText()
    {
        if (items.itemType == Items.ItemType.Blood)
        {
            var quickItem = quickitem.item5;
            var quickItemIcon = quickItem.transform.Find("ItemIcon").GetComponent<Image>();
            var quickItemCountText = quickItem.transform.Find("CountItem").GetComponent<TextMeshProUGUI>();
            quickItemIcon.sprite = items.icon;
            quickItemCountText.text = items.count.ToString();
            
        }
        if (items.itemType == Items.ItemType.Bullet)
        {
            var quickItem = quickitem.item6;
            var quickItemIcon = quickItem.transform.Find("ItemIcon").GetComponent<Image>();
            var quickItemCountText = quickItem.transform.Find("CountItem").GetComponent<TextMeshProUGUI>();
            quickItemIcon.sprite = items.icon;
            quickItemCountText.text = items.count.ToString();
        }

    }
}
