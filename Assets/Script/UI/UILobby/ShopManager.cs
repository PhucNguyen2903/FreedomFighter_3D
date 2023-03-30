using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    public static ShopManager Instance => instance;


    public List<ItemShop> itemShopList = new List<ItemShop>();
    public ShopItemController[] shopItems;
    public Transform shopContent;
    public GameObject shopItemPrefab;
    public PlayerInfo PlayerInfo;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI PointText;
    public TextMeshProUGUI ticketText;

    private void Start()
    {
        ShopManager.instance = this;
        FindPlayerInfo();
        SetupPlayerInfo();
        ShopUI();
    }

    private void Update()
    {
        SetupPlayerInfo();
    }



    public void FindPlayerInfo()
    {
        var playerInfo = GameObject.Find("PlayerInfo");
        PlayerInfo = playerInfo.GetComponent<PlayerInfo>();
      
    }
    public virtual void ShopUI()
    {
         DestroyContent(shopContent);
        foreach (ItemShop item in this.itemShopList)
        {
            GameObject itemShop = Instantiate(shopItemPrefab, shopContent);
            var gunName = itemShop.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            var PriceText = itemShop.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            var GunImage = itemShop.transform.GetChild(6).GetComponent<Image>();

            gunName.text = item.itemName;
            PriceText.text = item.price.ToString();
            GunImage.sprite = item.icon;

        }
        SetShopItems();
    }
    public virtual void SetShopItems()
    {
        shopItems = shopContent.GetComponentsInChildren<ShopItemController>();

        for (int i = 0; i < itemShopList.Count; i++)
        {
            shopItems[i].AddItem(itemShopList[i]);
        }
    }

    public virtual void ResetShopItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            Destroy(shopItems[i].gameObject);
        }
        shopItems = null;
    }

    public void DestroyContent(Transform content)
    {
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }


    public virtual void SetupPlayerInfo()
    {
        goldText.text = PlayerInfo.gold.ToString();
        PointText.text = PlayerInfo.point.ToString();
        ticketText.text = PlayerInfo.Ticket.ToString();
    }



}
