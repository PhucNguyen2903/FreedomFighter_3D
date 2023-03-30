using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StorageManager : ShopManager
{
    private static StorageManager instance;
    public static StorageManager Instance => instance;

    [SerializeField] Transform equipContent;
    [SerializeField] GameObject equipPrefab;
    [SerializeField] ItemShop firstItem;

    public List<ItemShop> storageList;
    public List<ItemShop> equipedList;



    private void Awake()
    {
        StorageManager.instance = this;
        FindPlayerInfo();
        SetupPlayerInfo();
        ShopUI();
    }
    private void Start()
    {
        equipedList = PlayerInfo.ListGun;
        storageList = PlayerInfo.ListStorage;
        if (equipedList.Count < 1)
        {
            equipedList.Add(firstItem);
        }
        EquipUI();
        
    }


    public void AddStorage(ItemShop itemAdd)
    {
        itemAdd.statusIN = 1;
        if (CheckTypeItem(itemAdd))
        {
            ItemCount(itemAdd);
        }
        else
        {
            storageList.Add(itemAdd);
        }


    }
    public void AddStorageFromBlackMarket(ItemShop itemAdd)
    {
        if (CheckTypeItem(itemAdd))
        {
            ItemCount(itemAdd);
        }
        else
        {
            storageList.Add(itemAdd);
        }
    }

    public void RemoveStorage(ItemShop itemRemove)
    {
        if (CheckTypeItem(itemRemove))
        {
            ItemCountDevide(itemRemove);
        }
        else
        {
            this.storageList.Remove(itemRemove);
        }
    }

    public override void ShopUI()
    {
        DestroyContent(shopContent);
        foreach (ItemShop item in this.storageList)
        {
            GameObject itemShop = Instantiate(shopItemPrefab, shopContent);
            var gunName = itemShop.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            var GunImage = itemShop.transform.GetChild(7).GetComponent<Image>();
            var itemstoragecontroller = itemShop.GetComponent<StorageItemController>();
            var GunStatus = itemShop.transform.GetChild(4).Find("Fill").GetComponent<Image>();

            GunStatus.fillAmount = item.statusIN;
            gunName.text = item.itemName;
            GunImage.sprite = item.icon;
            itemstoragecontroller.AddItem(item);

            if (CheckTypeItem(item))
            {
                itemShop.transform.GetChild(8).gameObject.SetActive(true);
                var itemAmounttext = itemShop.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
                itemAmounttext.text = item.count.ToString();
            }
        }

    }

    public void EquipUI()
    {
        DestroyContent(equipContent);
        foreach (ItemShop item in this.equipedList)
        {
            GameObject itemShop = Instantiate(equipPrefab, equipContent);
            var gunName = itemShop.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            var gunImage = itemShop.transform.GetChild(3).GetComponent<Image>();
            var itemController = itemShop.GetComponent<StorageItemController>();

            gunName.text = item.itemName;
            gunImage.sprite = item.icon;
            itemController.AddItem(item);
            itemController.SetStatus(item.statusIN);
        }

    }

    public void Equip(ItemShop itemShop)
    {
        equipedList.Add(itemShop);
        storageList.Remove(itemShop);
    }

    public void Equipdata(ItemShop itemShop)
    {
        this.equipedList.Add(itemShop);
        Debug.Log("EquipItemmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
        Debug.Log("Number of items in equipedList: " + equipedList.Count);
    }

    public void RemoveEquip(ItemShop itemShop)
    {
        equipedList.Remove(itemShop);
        storageList.Add(itemShop);
    }

    public List<ItemShop> TakeEquipItemList()
    {
        return equipedList;
    }
     public List<ItemShop> TakeStorageItemList()
    {
        Debug.Log("takeStorageList");
        return storageList;
    }

    public ItemShop TakeHealthBox()
    {
        foreach (ItemShop item in this.storageList)
        {
            if (item.itemType == ItemShop.ItemType.Blood)
            {
                return item;
            }
        }
        return null;
    }
    public ItemShop TakeBulletBox()
    {
        foreach (ItemShop item in this.storageList)
        {
            if (item.itemType == ItemShop.ItemType.Bullet)
            {
                return item;
            }
        }
        return null;
    }


    public void ItemCount(ItemShop item)
    {
        ItemShop itemExistInlist = this.storageList.Find((x) => x.id == item.id);
        if (itemExistInlist == null)
        {
            this.storageList.Add(item);
            item.count = 1;
        }
        else
        {
            CountIteminList(item);

        }
    }
    public void ItemCountDevide(ItemShop item)
    {
        if (item.count < 1)
        {
            this.storageList.Remove(item);
        }
        else
        {
            CountDevideIteminList(item);
        }

    }

    public void CountIteminList(ItemShop item)
    {
        foreach (ItemShop iteminlist in this.storageList)
        {
            if (iteminlist.id == item.id)
            {
                iteminlist.count += 1;
            }

        }
    }
    public void CountDevideIteminList(ItemShop item)
    {
        foreach (ItemShop iteminlist in this.storageList)
        {
            if (iteminlist.id == item.id)
            {
                iteminlist.count -= 1;

            }

        }
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
