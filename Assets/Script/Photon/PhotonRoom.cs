using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEditor;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TMP_InputField findroomInput;
    public TMP_InputField playerNameChatInput;
    public TMP_InputField numofPlayerInput;
    public GameObject PlayerInfoPrefab;
    public Transform roomContent;
    public UIRoomProfile roomPrefab;
    public List<RoomInfo> updatedRooms;
    public List<RoomProfile> rooms = new List<RoomProfile>();
    public Transform ListPlayerContent;
    public GameObject playerNamePrefab;
    public PhotonChatManager chatManager;
    public List<string> playerNameList = new List<string>();
    [SerializeField] Transform FirstMenuPlayerContent;
    [SerializeField] GameObject FirstMenuPlayerPrefab;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] SetPlayerInfo setPlayerinfo;
    [SerializeField] UIManager UImanager;

    public static PhotonRoom instance;
    private void Awake()
    {
        PhotonRoom.instance = this;
        numofPlayerInput.text = "2";
        //UImanager.OnclickBack();
        InvokeRepeating(nameof(CallCursor),10f,1f);
    }

    private void Start()
    {
        
    }

    public virtual void Create()
    {
        string playerName = PhotonNetwork.LocalPlayer.NickName;
        string roomName = roomNameInput.text + playerName;
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            roomName = "Room" + Random.Range(0, 100) + playerName;
        }
        Debug.Log(transform.name + ": create" + roomName);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)int.Parse(numofPlayerInput.text);
        PhotonNetwork.CreateRoom(roomName, roomOptions);

    }

    public void CallCursor()
    {
        Cursor.visible = true;
    }

    public virtual void JoinRoom()
    {

        string roomName = FindRoom(findroomInput.text);
        Debug.Log(transform.name + ": join" + roomName);     
        PhotonNetwork.JoinRoom(roomName);
        //if (PhotonNetwork.InRoom)
        //{
        //    Debug.Log("Inroom");
        //}

    }
    public virtual void JoinRoom(string roomName)
    {
        Debug.Log(transform.name + ": join" + roomName);
        PhotonNetwork.JoinRoom(roomName);
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Inroom");
        }

    }

    public virtual void Leave()
    {
        string roomName = roomNameInput.text;
        Debug.Log(transform.name + ": leave" + roomName);
        //if (PhotonNetwork.InRoom)
        //{
        //}
        //if (PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount <= 0)
        //{

        //}
        PhotonNetwork.LeaveRoom();

    }

    public virtual void StartGame()
    {
        Debug.Log(transform.name + ": Start Game");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel("ZoombieShooter");
        }
        else
            Debug.LogWarning("You are not MasterClient");
       
    }
  


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        this.updatedRooms = roomList;
        foreach (RoomInfo roomInfo in updatedRooms)
        {
            if (roomInfo.RemovedFromList)
            {
                this.RoomRemove(roomInfo);
            }
            else
            {
                this.RoomAdd(roomInfo);
                string playerName = TakePlayerName(roomInfo.Name);
                AddPlayerName(playerName);

            }

        }

        this.UpdateRoomProfileUI();
        this.UpdatepPlayerListInFirstMenu();
    }

    protected virtual void RoomAdd(RoomInfo roomInfo)
    {
        RoomProfile roomProfile;
        roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile != null) return;

        roomProfile = new RoomProfile
        {
            name = TakeRoomName(roomInfo.Name), /* roomInfo.Name,*/
            playerCount = roomInfo.PlayerCount,
            maxPlayer = roomInfo.MaxPlayers

        };

        this.rooms.Add(roomProfile);
    }

    public virtual void UpdateRoomProfileUI()
    {
        foreach (Transform child in roomContent)
        {
            Destroy(child.gameObject);
        }
        foreach (RoomProfile roomProfile in this.rooms)
        {
            UIRoomProfile uiRoomProfile = Instantiate(this.roomPrefab/*,roomContent*/);
            uiRoomProfile.SetRoomProfile(roomProfile);
            uiRoomProfile.transform.SetParent(this.roomContent);
            uiRoomProfile.transform.Find("MaxPlayerText").GetComponent<TextMeshProUGUI>().text = roomProfile.playerCount.ToString() + "/" + roomProfile.maxPlayer.ToString();
        }
    }

    protected virtual void RoomRemove(RoomInfo roomInfo)
    {
        RoomProfile roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile == null) return;
        this.rooms.Remove(roomProfile);
    }

    protected virtual RoomProfile RoomByName(string name)
    {
        string roomName = TakeRoomName(name);
        foreach (RoomProfile roomProfile in this.rooms)
        {
            if (roomProfile.name == roomName) return roomProfile;
        }
        return null;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        GameObject newplayerNameUI = Instantiate(playerNamePrefab, ListPlayerContent);
        newplayerNameUI.GetComponentInChildren<TextMeshProUGUI>().text = newPlayer.NickName;
        AddPlayerName(newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach (Transform child in ListPlayerContent)
        {
            string otherPlayerUi = child.GetComponentInChildren<TextMeshProUGUI>().text;
            if (otherPlayerUi == otherPlayer.NickName)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public override void OnJoinedLobby()
    {

        string playerNickname = PhotonNetwork.NickName;
        if (string.IsNullOrEmpty(playerNickname))
        {
            PhotonNetwork.NickName = "Fighter " + " " +Random.Range(0, 100).ToString();
            playerNickname = PhotonNetwork.NickName;
        }
        AddPlayerName(playerNickname);
        chatManager.SetUserName(PhotonNetwork.LocalPlayer.NickName);
        Debug.Log(PhotonNetwork.NickName + " are in lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is Created");
        //if (PhotonNetwork.InRoom)
        //{
        //    UIManager.Instnace.ObserverCallBack("RoomPopup");
        //}
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " join Room");
        if (PhotonNetwork.InRoom)
        {
            UIManager.Instnace.ObserverCallBack("RoomPopup");
            PlayerInfo.Instance.RemoveAllItemEquip();
            List<ItemShop> list = StorageManager.Instance.TakeEquipItemList();
            List<ItemShop> storageList = StorageManager.Instance.TakeStorageItemList();
            PlayerInfo.Instance.SetITem(list,storageList);
        }
        DestroyContent(ListPlayerContent);
        //foreach (Transform child in ListPlayerContent)
        //{
        //    Destroy(child.gameObject);
        //}
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject playerNameUI = Instantiate(playerNamePrefab, ListPlayerContent);
            playerNameUI.GetComponentInChildren<TextMeshProUGUI>().text = p.NickName;
            if (p.NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                playerNameUI.transform.Find("MeIcon").gameObject.SetActive(true);
                AddPlayerName(p.NickName);
            }
        }
       
       
    }

    public void ReturnFromGamePlayListPlayerUpdata()
    {
        DestroyContent(ListPlayerContent);
        //foreach (Transform child in ListPlayerContent)
        //{
        //    Destroy(child.gameObject);
        //}
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject playerNameUI = Instantiate(playerNamePrefab, ListPlayerContent);
            playerNameUI.GetComponentInChildren<TextMeshProUGUI>().text = p.NickName;
            if (p.NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                playerNameUI.transform.Find("MeIcon").gameObject.SetActive(true);
                AddPlayerName(p.NickName);
            }
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " left room");


        if (!PhotonNetwork.InRoom)
        {
            UIManager.Instnace.ObserverCallBack("FirstMenu");
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("JoinLobby again");
        }
    }

    public void DestroyContent(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdatepPlayerListInFirstMenu()
    {
        DestroyContent(FirstMenuPlayerContent);
        foreach (string name in playerNameList)
        {
            Debug.Log(name);
            GameObject obj = Instantiate(FirstMenuPlayerPrefab, FirstMenuPlayerContent);
            var playerName = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            var playerSelf = obj.transform.GetChild(1).gameObject;
            playerName.text = name;

            if (name == PhotonNetwork.LocalPlayer.NickName)
            {
                playerSelf.SetActive(true);
            }

        }
        Debug.Log("UpdatepPlayerListInFirstMenu...........................");
    }

    public void AddPlayerName(string name)
    {
        if (!playerNameList.Contains(name))
        {
            playerNameList.Add(name);
        }
    }

    public string TakePlayerName(string name)
    {
        int countString = name.LastIndexOf('F');
        Debug.Log("lengthOfroomName: " + countString);
        string playerName = name.Substring(countString);
        return playerName;
    }

    public string TakeRoomName(string name)
    {
        int countString = name.LastIndexOf('F');
        string roomName = name.Substring(0,countString);
        return roomName;
    }

    public string FindRoom(string roomName)
    {
        foreach (RoomInfo room in updatedRooms)
        {
            string roomNameInlist = TakeRoomName(room.Name);
           
            if (roomNameInlist == roomName)
            {
                return room.Name;
            }
        }
        return null;
    }

    




}
