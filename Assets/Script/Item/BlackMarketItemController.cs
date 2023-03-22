using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketItemController : ShopItemController
{
    float status;
    [SerializeField] GameObject prefabUI;
 
    

    public void Select()
    {
        BlackMarketManager.Instance.SelectItem(itemShop);
        BlackMarketManager.Instance.TakingItemBlackMarket(itemShop);
    }

    public void Take()
    {
        StorageManager.Instance.AddStorageFromBlackMarket(itemShop);
        BlackMarketManager.Instance.RemoveGistList(itemShop);
        Destroy(prefabUI);
    }

    public void SetStatus(float setStatus)
    {
        status = setStatus;
    }

    
}
