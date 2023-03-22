using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class GunSwitcher : MonoBehaviourPunCallbacks
{
    public GameObject[] guns;
    public List<GameObject> MyItem = new List<GameObject>();
    public GameObject mainCamera;
    public PhotonView PV;
    int gunnumber = 1;
    string WeaponName;
    float foV;
    public InventoryItemController inventoryItemController;
    public List<ItemShop> list;
    //public PlayerSingleton playerSingleton;


    private void OnEnable()
    {
        if (PV.IsMine)
        {
            TakeGunList();
        }
    }
    void Start()
    {
        if (PV.IsMine)
        {
            foV = mainCamera.GetComponent<Camera>().fieldOfView;
        }
        if (MyItem.Count < 1) return;
        WeaponName = MyItem[0].transform.name;

    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < guns.Length; i++)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
        //    {
        //        SetActiveGun(i);
        //    }
        //}

    }

    [PunRPC]
    public void SetPlayerInfo(string gunList)
    {
        Data Mydata = JsonUtility.FromJson<Data>(gunList);
        list = Mydata.GunItem;
        if (list.Count < 1) return;
        foreach (ItemShop item in list)
        {
            if (item != null)
            {
                PV.RPC("GetRunByname", RpcTarget.All, item.name);
            }
        }

    }

    public void GunSwitching()
    {
        for (int i = 0; i < MyItem.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
            {
                PV.RPC("SetActiveGun",RpcTarget.AllBuffered,i);
                WeaponName = MyItem[i].transform.name;
            }
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            SetActiveGun((int)changedProps["gunIndex"]);
        }
    }

    [PunRPC]
    public void SetActiveGun(int gunIndex)
    {
        for (int i = 0; i < MyItem.Count; i++)
        {
            bool isActive = (i == gunIndex);
            //guns[i].SetActive(isActive);
            MyItem[i].SetActive(isActive);

            if (isActive)
            {
                MyItem[i].SendMessage("OnGunSelected", SendMessageOptions.DontRequireReceiver);
                foV = 60f;
                gunnumber = gunIndex;
            }

        }

        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("gunIndex", gunIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

    }

    [PunRPC]
    public void GetRunByname(string name)
    {
        foreach (GameObject gun in this.guns)
        {
            if (gun.name == name)
            {
                MyItem.Add(gun);
                Debug.Log("ADD" + gun.name);
            }
        }
        if (MyItem.Count < 1) return;
        MyItem[0].gameObject.SetActive(true);

    }
    public void TakeGunList()
    {
        string GunList = PlayerInfo.Instance.TakeList();
        PV.RPC("SetPlayerInfo", RpcTarget.AllBuffered, GunList);
    }

    public void SetGunbutton()
    {
        if (WeaponName == null) return;
        if (WeaponName == "Grenade_Launcher")
        {
            GrenadeLauncher grenade = guns[0].gameObject.GetComponent<GrenadeLauncher>();
            GunAmmo gunAmmo = guns[0].gameObject.GetComponent<GunAmmo>();
            inventoryItemController.SetGunAmmo(gunAmmo);
            grenade.GrenadeButton();
        }
        if (WeaponName == "Bullpup_Assault_Rifle")
        {
            AutomaticShooting automatic = guns[1].gameObject.GetComponent<AutomaticShooting>();
            GunAmmo gunAmmo = guns[1].gameObject.GetComponent<GunAmmo>();
            inventoryItemController.SetGunAmmo(gunAmmo);
            automatic.AutomatcShoot();

        }
        if (WeaponName == "Bolt-Action_Sniper_Rifle")
        {
            Sniper snip = guns[2].gameObject.GetComponent<Sniper>();
            GunAmmo gunAmmo = guns[2].gameObject.GetComponent<GunAmmo>();
            inventoryItemController.SetGunAmmo(gunAmmo);
            snip.SnipperButton();
        }
        if (WeaponName == "arms@flamethrower")
        {
            FLamethrower flame = guns[3].gameObject.GetComponent<FLamethrower>();
            GunAmmo gunAmmo = guns[3].gameObject.GetComponent<GunAmmo>();
            inventoryItemController.SetGunAmmo(gunAmmo);
            flame.FlameButton();
        }
        if (WeaponName == "arms@pistol1")
        {
            Pistol pis = guns[4].gameObject.GetComponent<Pistol>();
            GunAmmo gunAmmo = guns[4].gameObject.GetComponent<GunAmmo>();
            inventoryItemController.SetGunAmmo(gunAmmo);
            pis.PistolButton();
        }
        if (WeaponName == "arms@revolver")
        {
            Pistol pis = guns[5].gameObject.GetComponent<Pistol>();
            GunAmmo gunAmmo = guns[5].gameObject.GetComponent<GunAmmo>();
            inventoryItemController.SetGunAmmo(gunAmmo);
            pis.PistolButton();
        }

        if (WeaponName == "Machete")
        {
            MleeWeapons mlee = guns[7].gameObject.GetComponent<MleeWeapons>();
            inventoryItemController.SetGunAmmo();
            mlee.MleeButton();
        }
        if (WeaponName == "ButcherKnife")
        {
            MleeWeapons mlee = guns[6].gameObject.GetComponent<MleeWeapons>();
            inventoryItemController.SetGunAmmo();
            mlee.MleeButton();
        }
        if (WeaponName == "TacticalKnife")
        {
            MleeWeapons mlee = guns[8].gameObject.GetComponent<MleeWeapons>();
            inventoryItemController.SetGunAmmo();
            mlee.MleeButton();
        }
        if (WeaponName == "Shovel")
        {
            MleeWeapons mlee = guns[9].gameObject.GetComponent<MleeWeapons>();
            inventoryItemController.SetGunAmmo();
            mlee.MleeButton();
        }
        if (WeaponName == "Crowbar")
        {
            MleeWeapons mlee = guns[10].gameObject.GetComponent<MleeWeapons>();
            inventoryItemController.SetGunAmmo();
            mlee.MleeButton();
        }
        if (WeaponName == "Hammer")
        {
            MleeWeapons mlee = guns[11].gameObject.GetComponent<MleeWeapons>();
            inventoryItemController.SetGunAmmo();
            mlee.MleeButton();
        }
       
    }
}
