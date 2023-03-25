using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using static UnityEngine.ParticleSystem;

public class ItemLooter : MonoBehaviour
{
    public PhotonView PV;
    public float looterRadius;
    public InventoryManager inventoryManager;
    public QuickItems quickItems;
    InventoryItemController item5;
    InventoryItemController item6;
    private void Awake()
    {
        item5 = quickItems.item5.GetComponent<InventoryItemController>();
        item6 = quickItems.item5.GetComponent<InventoryItemController>();
    }
    private void Update()
    {
       
    }

    public void Looter()
    {
        
        if (!PV.IsMine) return;
        if (Input.GetKeyDown(KeyCode.E))
        {

            PV.RPC("CheckLooter", RpcTarget.OthersBuffered);
            PickUp();
        }
    }


    [PunRPC]
    void CheckLooter()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, looterRadius);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            ItemPickup items = affectedObjects[i].GetComponent<ItemPickup>();
            if (items != null)
            {
                items.Pickup();
            }
        }
    }




    void PickUp()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, looterRadius);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            ItemPickup items = affectedObjects[i].GetComponent<ItemPickup>();
            if (items != null)
            {
                inventoryManager.Add(items.items);
                item5.UpdateCountText();
                item6.UpdateCountText();
                inventoryManager.UpdateListItem();
                items.Pickup();
            }
        }
    }
   
}
