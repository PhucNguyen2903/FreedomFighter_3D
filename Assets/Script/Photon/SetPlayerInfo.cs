using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class SetPlayerInfo : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public PlayerInfo playerInfoPrefab;
    public PlayerInfo playerInfoloby;


    private void Awake()
    {
         PlayerPrefab = Resources.Load<GameObject>("PlayerPrefab/Player");
         playerInfoPrefab = PlayerPrefab.GetComponent<PlayerInfo>();
       
    }



    public void SetScript()
    {
        PlayerPrefab = Resources.Load<GameObject>("PlayerPrefab/Player");
        playerInfoPrefab = PlayerPrefab.GetComponent<PlayerInfo>();
        int goldplayerInfoloby = playerInfoloby.gold;
        FieldInfo field = typeof(PlayerInfo).GetField("gold", BindingFlags.Public | BindingFlags.Instance);
        field.SetValue(playerInfoPrefab, goldplayerInfoloby);


        //List<ItemShop> itemListplayerInfoloby = playerInfoloby.itemListPlayer;
        //int goldplayerInfoloby = playerInfoloby.gold;
        //int pointplayerInfoloby = playerInfoloby.point;
        //int ticketplayerInfoloby = playerInfoloby.Ticket;

        //List<ItemShop> itemListplayerPrefab = playerInfoPrefab.itemListPlayer;
        //int goldplayerInfoPrefab = playerInfoPrefab.gold;
        //int pointplayerInfoPrefab = playerInfoPrefab.point;
        //int ticketplayerInfoPrefab = playerInfoPrefab.Ticket;
        //foreach (var item in itemListplayerInfoloby)
        //{
        //    itemListplayerPrefab.Add(item);
        //    Debug.Log("ChagePlayerInfoPrefab");
        //}
        //goldplayerInfoPrefab = goldplayerInfoloby;
        //pointplayerInfoPrefab = pointplayerInfoloby;
        //ticketplayerInfoPrefab = ticketplayerInfoloby;
    }

}
