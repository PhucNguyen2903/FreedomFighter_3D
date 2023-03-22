using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView photonView;
    public PlayerJump playerjump;
    public RotateByMouse rotateByMouse;
    public PlayerController playerController;
    public QuickItems quickitems;
    public Camera camera;
    public AudioListener audioListener;
    public GunSwitcher gunSwitcher;
    public GameObject MinimapCamera;
    public GameObject sniperCamera;
    public GameObject Canvas;
    public InventoryManager inventoryManager;
    public List<GameObject> Guntext = new List<GameObject>();




    private void Start()
    {
        if (!this.photonView.IsMine)
        {
            Destroy(this.camera);
            Destroy(this.audioListener);
            MinimapCamera.SetActive(false);
            sniperCamera.SetActive(false);
            Canvas.SetActive(false);
         //   inventoryManager.SetActive(false);
            foreach  (GameObject guntextIn in this.Guntext)
            {
                guntextIn.gameObject.SetActive(false);
            }
        }
    }


    private void Update()
    {
        OwenerController();
    }

    protected void OwenerController()
    {
        if (!this.photonView.IsMine) return;
        this.playerjump.KeyJump();
        this.rotateByMouse.MouseRotating();
        this.playerController.MoingBYKey();
        this.gunSwitcher.GunSwitching();
        this.gunSwitcher.SetGunbutton();
        this.quickitems.QuickButton();
        this.inventoryManager.Inventorybutton();
        
    }
}
