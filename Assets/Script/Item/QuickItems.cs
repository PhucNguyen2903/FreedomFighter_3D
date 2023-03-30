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
    public GameObject EscPanel;
    bool isActive = true;


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
            InventoryItemController itemFcontroller = item5.GetComponent<InventoryItemController>();
            if (!CanUseItemF(itemFcontroller)) return;
            itemFcontroller.UseItem();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryItemController itemGcontroller = item6.GetComponent<InventoryItemController>();
            if (!CanUseItemG(itemGcontroller)) return;
            itemGcontroller.UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeButton(isActive);

        }
    }


    public void EscapeButton( bool isACtive)
    {
        EscPanel.SetActive(isACtive);
        Cursor.visible = isACtive;
        isActive = !isActive;
    }



    bool CanUseItemG(InventoryItemController itemcontroller)
    {
        int numOfMag = itemcontroller.gunAmmo.numOfMag;
        int magSize = itemcontroller.gunAmmo.magSize;
        if (numOfMag != null && numOfMag < magSize *2)
        {
            return true;
        }
        return false;

    }
    bool CanUseItemF(InventoryItemController itemcontroller)
    {
        int Hp = itemcontroller.playerHealth.Hp;
        int maxHP = itemcontroller.playerHealth.maxHP;
        if (Hp < maxHP)
        {
            return true;
        }
        return false;

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
