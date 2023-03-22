using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    public Transform itemContent;
    public GameObject inventoryItem;
    public Toggle enableRemove;
    public GameObject inventoryIcon;
    public GameObject inventory;
    public int countItem = 0;
    private bool isAitive = false;
    [SerializeField] private Items healthItem;
    [SerializeField] private Items bulletItem;
    [SerializeField] private PhotonView PV;
    public InventoryItemController[] inventoryItems;
    

    public List<Items> items = new List<Items>();
    private void Awake()
    {
        InventoryManager.instance = this;
        if (PV.IsMine)
        {
            TakeItem();
        }
    }
    private void Update()
    {
       // Inventorybutton();
    }

    public void Add(Items item)
    {
        Items itemExistInlist = this.items.Find((x) => x.id == item.id);
        if (itemExistInlist == null)
        {
            items.Add(item);
            item.count = item.amount;
            //item.count += item.amount;
        }
        else
        {
            CountIteminList(item);

        }
    }

    public void Remove(Items item)
    {
        item.count--;
        if (item.count < 1)
        {
            items.Remove(item);
        }

    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Items item in this.items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCountText = obj.transform.Find("CountItem").GetComponent<TextMeshProUGUI>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            itemCountText.text = item.count.ToString();
            itemIcon.sprite = item.icon;
            if (enableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        SetInventoryItems();
    }

    public void UpdateListItem()
    {

        foreach (Items item in this.items)
        {
            if (item.itemType == Items.ItemType.Blood)
            {
                var quickItem = QuickItems.Instance.item5;
                var quickItemIcon = quickItem.transform.Find("ItemIcon").GetComponent<Image>();
                var quickItemCountText = quickItem.transform.Find("CountItem").GetComponent<TextMeshProUGUI>();
                var inventoryItems5 = quickItem.GetComponent<InventoryItemController>();
                inventoryItems5.AddItem(item);
                quickItemIcon.sprite = item.icon;
                quickItemCountText.text = item.count.ToString();

            }
            if (item.itemType == Items.ItemType.Bullet)
            {
                var quickItem6 = QuickItems.Instance.item6;
                var quickItemIcon6 = quickItem6.transform.Find("ItemIcon").GetComponent<Image>();
                var quickItemCountText6 = quickItem6.transform.Find("CountItem").GetComponent<TextMeshProUGUI>();
                var inventoryItems6 = quickItem6.GetComponent<InventoryItemController>();
                inventoryItems6.AddItem(item);
                quickItemIcon6.sprite = item.icon;
                quickItemCountText6.text = item.count.ToString();

            }

        }

    }

    public void EnableItemRemove()
    {
        if (enableRemove.isOn)
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }

        }
        else
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("InventoryRun....");
            inventoryItems[i].AddItem(items[i]);
        }
    }

    public void CountIteminList(Items item)
    {
        foreach (Items iteminlist in this.items)
        {
            if (iteminlist.id == item.id)
            {
                iteminlist.count += item.amount;

            }

        }
    }

    public void Inventorybutton()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!isAitive);
            ListItems();
            inventoryIcon.SetActive(!isAitive);
            isAitive = !isAitive;
        }

    }

    public void TakeItem()
    {
        string itemdata = PlayerInfo.Instance.TakeList();
        PV.RPC("TakeItemdata", RpcTarget.All, itemdata);
    }

    public void SetItemcount(ItemShop item)
    {
        if (item.itemType == ItemShop.ItemType.Bullet)
        {
            this.bulletItem.count = item.count;
            Debug.Log(this.bulletItem.count + "XXXX" + item.count);
        }
        if (item.itemType == ItemShop.ItemType.Blood)
        {
            this.healthItem.count = item.count;
            Debug.Log(this.healthItem.count + "XXXX" + item.count);
        }
        
    }
    [PunRPC]
    public void TakeItemdata(string data)
    {
        Data Mydata = JsonUtility.FromJson<Data>(data);
        ItemShop healthBox = Mydata.healthBox;
        ItemShop bulletbox = Mydata.bulletbox;
        if (healthBox != null)
        {
            this.Add(healthItem);
            SetItemcount(healthBox);
        }
        if (bulletbox != null)
        {       
            this.Add(bulletItem);
            SetItemcount(bulletbox);
            Debug.Log(bulletItem.count + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        }
        UpdateListItem();
    }
}
