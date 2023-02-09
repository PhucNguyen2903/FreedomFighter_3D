using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public InventoryItemController[] inventoryItems;

    public List<Items> items = new List<Items>();
    private void Awake()
    {
        InventoryManager.instance = this;
    }
    private void Update()
    {
        Inventorybutton();
    }

    public void Add(Items item)
    {
        Items itemExistInlist = this.items.Find((x) => x.id == item.id);
        if (itemExistInlist == null)
        {
            items.Add(item);
            item.count += item.amount;
        }
        else
        {
            CountIteminList(item);
        }

        


        //if(items.Count == 0)
        //{
        //    items.Add(item);
        //    item.count = 0;
        //}
       
        
        //items.Add(item);
    }

    public void Remove(Items item)
    {
        items.Remove(item);
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

    public void EnableItemRemove()
    {
        if (enableRemove.isOn)
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true) ;
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
            inventoryItems[i].AddItem(items[i]);
        }
    }

    private void CountIteminList(Items item)
    {
        foreach (Items iteminlist in this.items)
        {
            if (iteminlist.id == item.id)
            {
                iteminlist.count += item.amount;
                
            }

        }
    }



    private void Inventorybutton()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.active);
            ListItems();
            inventoryIcon.SetActive(!inventoryIcon.active);
        }

    }
}
