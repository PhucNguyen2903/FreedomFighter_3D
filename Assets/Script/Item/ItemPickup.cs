using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Items items;

    public float looterRadius;

    public void Pickup()
    {
        InventoryManager.Instance.Add(items);
        //Destroy(gameObject);
        ZombieController.Instance.dropItemSpawner.Despawn(transform);

    }


}
