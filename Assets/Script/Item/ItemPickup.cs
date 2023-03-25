using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Items items;

    public float looterRadius;
    GameObject item5;
    GameObject item6;

    private void Awake()
    {
        item5 = QuickItems.Instance.item5;
        item6 = QuickItems.Instance.item6;
    }


    public void Pickup()
    {
        //InventoryManager.Instance.Add(items);
        //InventoryManager.Instance.UpdateListItem();
        Debug.Log("DespawnItem");
        ZombieController.Instance.dropItemSpawner.Despawn(transform);
    }




}
