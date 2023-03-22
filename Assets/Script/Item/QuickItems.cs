using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuickItems : MonoBehaviour
{

    private static QuickItems instance;
    public static QuickItems Instance => instance;
    public PhotonView PV;
    public GameObject countItem5;
    public GameObject countItem6;
    public GameObject item5;
    public GameObject item6;


    private void Awake()
    {
        QuickItems.instance = this;

    }
    private void Update()
    {
      //  QuickButton();
       
    }


    public void UpdatecountText()
    {
        InventoryItemController item5controller = item5.GetComponent<InventoryItemController>();
        InventoryItemController item6controller = item6.GetComponent<InventoryItemController>();
    }


    public void QuickButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //if (!PV.IsMine) return;
            //PV.RPC("useItem5", RpcTarget.All);
            InventoryItemController item5controller = item5.GetComponent<InventoryItemController>();
            item5controller.UseItem();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryItemController item5controller = item6.GetComponent<InventoryItemController>();
            item5controller.UseItem();
        }
    }


    [PunRPC]
    public void useItem5()
    {
        InventoryItemController item5controller = item5.GetComponent<InventoryItemController>();
        item5controller.UseItem();
    }
    [PunRPC]
    public void useItem6()
    {
        InventoryItemController item5controller = item6.GetComponent<InventoryItemController>();
        item5controller.UseItem();
    }


}
