using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLooter : MonoBehaviour
{

    public float looterRadius;
    private void Update()
    {
        Looter();
    }

    public void Looter()
    {
        if (Input.GetKey(KeyCode.E))
        {
            CheckLooter();
        }
    }


    private void CheckLooter()
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

}
