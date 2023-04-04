using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class PlayerInPlay : MonoBehaviourPunCallbacks
{
    private static PlayerInPlay instance;
    public static PlayerInPlay Instance => instance;

    public float explosionRadius = 1000f;
    public List<int> playerViewID = new List<int>();
    public Transform PlayerInGameContent;
    public GameObject prefab;
    public GameObject playerResultPrefab;
    public Transform playerResultContent;
    public GameObject GameOverPopup;
    public List<PlayerUIManager> gameoverPopup = new List<PlayerUIManager>();
    public int numOfDead;
    [SerializeField] GameObject playerListRoomInfoPopup;
    public PhotonView PV;



    private void Awake()
    {
        PlayerInPlay.instance = this;

        StartCoroutine(waitUntil());
        InvokeRepeating("UpdateUIPlayer", 15f, 1f);
        numOfDead = ZombieController.Instance.enemySpawner.count;
    }

    private void Start()
    {

       
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
        TurnOnPlayerList();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (colliders.Length < 1) return;
        Debug.Log("Run...............");
        for (int i = 0; i < colliders.Length; i++)
        {
            FindPlayer(colliders[i]);
        }

        //PV.RPC("FindPlayerSingleton", RpcTarget.All);
    }
    public void FindPlayer(Collider colider)
    {
        // PhotonView playerPV = colider.GetComponent<PhotonView>();
        PlayerHealth playerHealth = colider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            PhotonView playerPV = colider.GetComponent<PhotonView>();
            PlayerUIManager player = colider.GetComponent<PlayerUIManager>();

            if (playerPV != null)
            {
                int viewID = playerPV.ViewID;
                if (!playerViewID.Contains(viewID))
                {
                    gameoverPopup.Add(player);
                    Debug.Log("AddPlayerSingleton: " + colider.GetComponent<PhotonView>().ViewID);
                    playerViewID.Add(viewID);
                }
            }
        }

    }

    public void AddListGameoverPopup(PlayerUIManager player)
    {
              gameoverPopup.Add(player);
    }

    
    public void CallPopupGameover()
    {
        PV.RPC("TurnOnGameOverUIAfterWaiting",RpcTarget.All);
    }
    
    [PunRPC]
    public void TurnOnGameOverUIAfterWaiting()
    {
        StartCoroutine(TurnOnGameOverUI());
    }

    public IEnumerator TurnOnGameOverUI()
    {
        yield return new WaitForSeconds(2f);
        
        foreach (var item in playerViewID)
        {
            UpdataGameOverUI(item);           
        }
        TurnOffPlayerList();
        GameOverPopup.SetActive(true);
    }
    public void UpdataGameOverUI(int item)
    {
        PhotonView playerPV = PhotonView.Find(item);
        GameObject obj = playerPV.gameObject;
        PlayerUIManager PlayerUImanager = playerPV.gameObject.GetComponent<PlayerUIManager>();

        GameObject playerResult = Instantiate(playerResultPrefab , playerResultContent);

        PlayerUImanager.timeCount.TimeStopStatus(true);

        var NamePlayerResult = playerResult.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var RankPlayerResult = playerResult.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var TimePlayerResult = playerResult.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        var ScorePlayerResult = playerResult.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        int playerScore = PlayerUImanager.scoreText.Score;
        int playertimeMinus = PlayerUImanager.TakeMinus();
        int playertimeSecond = PlayerUImanager.TakeSecond();

        int playerMinus = TimeMinusset(playertimeMinus);
        int playerSecond = TimeSecondSet(playertimeSecond);
        int playerRank = RankerList(playerScore);
        Debug.Log(playerScore + " " + playertimeMinus + "  " + playertimeSecond + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

        NamePlayerResult.text = playerPV.Owner.NickName;
        ScorePlayerResult.text = playerScore.ToString();
        TimePlayerResult.text =  string.Format("{0:00} : {1:00}", playerMinus, playerSecond);

        RankPlayerResult.text = playerRank.ToString();
    }

    void TurnOffPlayerList()
    {
        playerListRoomInfoPopup.SetActive(false);
    }
    void TurnOnPlayerList()
    {
        playerListRoomInfoPopup.SetActive(true);
    }

    public int RankerList(int score)
    {
        int count = 1;

        foreach (var item in playerViewID)
        {
            PhotonView playerPV = PhotonView.Find(item);
            PlayerUIManager PlayerUImanager = playerPV.gameObject.GetComponent<PlayerUIManager>();
            int playerScore = PlayerUImanager.scoreText.Score;
            if (score < playerScore)
            {
                count++;
            }
        }
        Debug.Log(count);
        return count;
    }

    public int TimeMinusset(int Minus)
    {
        foreach (var item in playerViewID)
        {
            PhotonView playerPV = PhotonView.Find(item);
            PlayerUIManager PlayerUImanager = playerPV.gameObject.GetComponent<PlayerUIManager>();
            int playertimeMinus = PlayerUImanager.TakeMinus();
            if (Minus < playertimeMinus)
            {
                return playertimeMinus;
            }
        }

        return Minus;

    }
    public int TimeSecondSet(int second)
    {
        foreach (var item in playerViewID)
        {
            PhotonView playerPV = PhotonView.Find(item);
            PlayerUIManager PlayerUImanager = playerPV.gameObject.GetComponent<PlayerUIManager>();
            int playertimeSecond = PlayerUImanager.TakeSecond();
            if (second < playertimeSecond)
            {
                return playertimeSecond;
            }
        }

        return second;

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
                UIObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = playerPV.Owner.NickName;    /*"Player " + viewId;*/
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


