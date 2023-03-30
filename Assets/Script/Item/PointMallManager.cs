using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointMallManager : ShopManager
{

    private static PointMallManager instance;
    public static PointMallManager Instance => instance;
    //public override void Awake()
    //{
    //    PointMallManager.instance = this;
    //    SetupPlayerInfo();
    //    ShopUI();
    //}
    private void Start()
    {
        PointMallManager.instance = this;
        FindPlayerInfo();
        SetupPlayerInfo();
        ShopUI();
    }

    public override void ShopUI()
    {
        foreach (Transform item in this.shopContent)
        {
            Destroy(item.gameObject);
        }

        foreach (ItemShop item in this.itemShopList)
        {
            GameObject itemShop = Instantiate(shopItemPrefab, shopContent);
            var gunName = itemShop.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            var PriceText = itemShop.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            var GunImage = itemShop.transform.GetChild(6).GetComponent<Image>();

            gunName.text = item.itemName;
            PriceText.text = (item.price * 2).ToString();
            GunImage.sprite = item.icon;

        }
        SetShopItems();
    }
}
