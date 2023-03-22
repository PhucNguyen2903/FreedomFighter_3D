using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class PlayerInPlay : MonoBehaviourPunCallbacks
{ 
    public float explosionRadius = 1000f;
    public List<int> playerViewID = new List<int>();
    public Transform PlayerInGameContent;
    public GameObject prefab;
   // public GameObject[] playerObjects;

    private void Awake()
    {
    //    TakePlayerList();
    //    UpdateUIPlayer();
       StartCoroutine(waitUntil());
       InvokeRepeating("UpdateUIPlayer", 15f, 1f);
    }

    private void Update()
    {
        
    }

   



    IEnumerator waitUntil()
    {
        yield return new WaitForSeconds(10f);
        CheckMap();
    }

    public void CheckMap()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (colliders.Length < 1) return;
        Debug.Log("Run...............");
        for (int i = 0; i < colliders.Length; i++)
        {
            FindPlayer(colliders[i]);
        }

    }
    public void FindPlayer(Collider colider)
    {
       // PhotonView playerPV = colider.GetComponent<PhotonView>();
        PlayerHealth playerHealth = colider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            PhotonView playerPV = colider.GetComponent<PhotonView>();
            if (playerPV != null)
            {
                int viewID = playerPV.ViewID;
                if (!playerViewID.Contains(viewID))
                {
                    playerViewID.Add(viewID);
                }
            }
        }
       
       
       
    }

    public void UpdateUIPlayer()
    {
        foreach (Transform playerInfo in this.PlayerInGameContent)
        {
            Destroy(playerInfo.gameObject);
        }
        foreach (int viewId in this.playerViewID)
        {
            PhotonView playerPV = PhotonView.Find(viewId);
            if (playerPV != null)
            {
                GameObject obj = playerPV.gameObject;
                PlayerHealth playerHealth = obj.gameObject.GetComponent<PlayerHealth>();
                int healthPoint = playerHealth.Hp;
                int MaxHealPoint = playerHealth.maxHP;
                float hPValue = (float)healthPoint / (float)MaxHealPoint;
                GameObject UIObj = Instantiate(prefab, PlayerInGameContent);
                UIObj.transform.GetChild(1).GetComponent<Image>().fillAmount = hPValue;
                UIObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Player " + viewId;
            }
        }

    }


    //public void UpdateUIPlayer()
    //{
    //    foreach (GameObject player in playerObjects)
    //    {
    //        PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>();
    //        int healthPoint = playerHealth.Hp;
    //        int MaxHealPoint = playerHealth.maxHP;
    //        float hPValue = healthPoint / MaxHealPoint;
    //        GameObject UIObj = Instantiate(prefab, PlayerInGameContent);
    //        UIObj.transform.GetChild(1).GetComponent<Image>().fillAmount = hPValue;
    //        UIObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = player.name;

    //    }
    //}

    //public void TakePlayerList()
    //{
    //    if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
    //    {
    //        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

    //        if (players != null && players.Length > 0)
    //        {
    //            playerObjects = new GameObject[players.Length];

    //            for (int i = 0; i < players.Length; i++)
    //            {

    //                PhotonView playerPV = PhotonView.Find(players[i].ActorNumber);
    //                if (playerPV != null && playerPV.Owner == players[i])
    //                {
    //                    Debug.Log("RunOK");
    //                    playerObjects[i] = playerPV.gameObject;

    //                }
    //            }
    //        }
    //    }
    //}



}


