using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopItem", menuName = "ItemShop/Create New ItemShop")]
public class ItemShop : ScriptableObject
{
    public int id;
    public string itemName;
    public int price;
    public int count;
    public float statusIN;
    public Sprite icon;
    public ItemType itemType;

    public enum ItemType
    {
        Blood,
        Bullet,
        Weapons,
        Gift,
    }


}


