using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlackMarketManager : ShopManager
{
    private static BlackMarketManager instance;
    public static BlackMarketManager Instance => instance;
    [SerializeField] Transform GiftContent;
    [SerializeField] GameObject GiftPrefab;
    public List<ItemShop> giftList = new List<ItemShop>();
    public List<ItemShop> supGiftList = new List<ItemShop>();
    public GameObject popupTakingGift;
    public Transform popupTakingGiftParent;
    public ItemShop giftSelect;
    public AudioSource soundSpin;
    public Image gunImage;
    Sprite beginIma;
    public Spin spin;
    public bool nextRotate;
    int changeGift = 0;

    
    private void Start()
    {
        BlackMarketManager.instance = this;
        SetupPlayerInfo();
        ShopUI();
        beginIma = gunImage.sprite;
    }

    public override void ShopUI()
    {
        foreach (Transform item in this.shopContent)
        {
            Destroy(item.gameObject);
        }
        foreach (ItemShop item in this.itemShopList)
        {
            GameObject itemShop = Instantiate(shopItemPrefab, this.shopContent);
            var gunName = itemShop.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            var GunStatus = itemShop.transform.GetChild(4).Find("Fill").GetComponent<Image>();
            var GunImage = itemShop.transform.GetChild(6).GetComponent<Image>();
            float randomStatus = Random.Range(0.1f, 0.8f);
            GunStatus.fillAmount = randomStatus;
            item.statusIN = randomStatus;
            gunName.text = item.itemName;
            GunImage.sprite = item.icon;
        }
        SetShopItems();
    }

    public override void SetupPlayerInfo()
    {
        ticketText.text = PlayerInfo.Ticket.ToString();
    }

    public void SelectItem(ItemShop itemshop)
    {
        gunImage.sprite = itemshop.icon;
    }

    public void OnclickTurn()
    {

        StartCoroutine(Turn());
    }
    public IEnumerator Turn()
    {

        ResetShopItems();
        yield return new WaitForSeconds(1f);
        ShopUI();
        gunImage.sprite = beginIma;
    }


    public void Rotate()
    {
        // if (nextRotate == false) return;
        nextRotate = false;
        int playerTicket = PlayerInfo.Instance.Ticket;
        if (playerTicket < 1) return;
        soundSpin.Play();
        spin.RotateNow();
        PlayerInfo.Instance.DevideTicket(1);
        SetupPlayerInfo();
        int gift = spin.indexGiftRandom;
        StartCoroutine(WaitingAnim(gift));

    }

    public void TakingItemBlackMarket(ItemShop itemShop)
    {
        giftSelect = itemShop;
    }


    public void TakeGift(int gift)
    {
        if (gift == 1)
        {
            giftSelect = supGiftList[2];
            giftList.Add(giftSelect);
        }
        if (gift == 2)
        {
            giftSelect = supGiftList[1];
            giftList.Add(giftSelect);
        }
        if (gift == 3)
        {
            giftSelect = supGiftList[0];
            giftList.Add(giftSelect);
        }

        if (gift == 4)
        {
            if (IsRandomGift(giftSelect))
            {
                giftSelect = RandomGift(giftSelect);
            }
            giftList.Add(giftSelect);
        }

    }

    public ItemShop RandomGift(ItemShop GiftSelect)
    {

        int random = Random.Range(0, itemShopList.Count);
        if (changeGift > 0)
        {
            GiftSelect = itemShopList[random];
            Debug.Log(GiftSelect.itemName);
            changeGift++;
            return GiftSelect;
        }

        if (GiftSelect == null)
        {
            GiftSelect = itemShopList[random];
            Debug.Log(GiftSelect.itemName);
            changeGift++;
            return GiftSelect;
        }


        return GiftSelect;
    }

    public bool IsRandomGift(ItemShop Giftselect)
    {
        if (Giftselect == null)
        {
            return true;
        }

        return false;
    }

    public void RemoveGistList(ItemShop itemShop)
    {
        giftList.Remove(itemShop);
    }

    public void UpdateGifUI()
    {
        DestroyGiftList();
        foreach (ItemShop item in this.giftList)
        {
            GameObject itemShop = Instantiate(GiftPrefab, this.GiftContent);
            var gunName = itemShop.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            var GunStatus = itemShop.transform.GetChild(4).Find("Fill").GetComponent<Image>();
            var GunImage = itemShop.transform.GetChild(6).GetComponent<Image>();
            var Itemcontroller = itemShop.GetComponent<BlackMarketItemController>();
            Itemcontroller.AddItem(item);
            float randomStatus = SetStatusIn(item);
            Itemcontroller.SetStatus(randomStatus);
            GunStatus.fillAmount = randomStatus;
            gunName.text = item.itemName;
            GunImage.sprite = item.icon;
        }

    }



    public void DestroyGiftList()
    {
        foreach (Transform itemIn in this.GiftContent)
        {
            Destroy(itemIn.gameObject);
        }
    }

    private float SetStatusIn(ItemShop itemShop)
    {
        float randomStatus = 1;
        if (itemShop.statusIN > 0)
        {
            return itemShop.statusIN;
        }
        else
        {
            return randomStatus;
        }

    }



    IEnumerator WaitingAnim(int gift)
    {
        yield return new WaitForSeconds(8f);
        GameObject popupPrefab = Instantiate(popupTakingGift, popupTakingGiftParent);
        TakeGift(gift);
        yield return new WaitForSeconds(0.5f);
        Destroy(popupPrefab);
        UpdateGifUI();
        // nextRotate = true;

    }

}
