using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class GunAmmo : Shooting
{
    private static GunAmmo instance;
    public static GunAmmo Instance => instance;

    public PhotonView PV;


    [SerializeField] public Animator anim;
    public int magSize;
    public int numOfMag;
    public AudioSource[] reloadSounds;
    public Shooting shooting;
    public UnityEvent loadAmmoChanged;

    private int _loadAmmoValue;
    private int _loadAmmo;
    private bool isReloading;
    public AmmoText ammoText;

    public int LoadAmmo
    {
        get => _loadAmmo;
        
        
        set
        {
            _loadAmmo = value;
            loadAmmoChanged.Invoke();
            if (_loadAmmo < 1 )
            {
                Reload();
            }
            else
            {
                //UnlockShooting();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       RefillAmmo();
       GunAmmo.instance = this;
       
    }

   

    public virtual void SingleFireAmmoCounter()
    {
        if (LoadAmmo == 0) return;
        LoadAmmo--;
    }
    public void LockShooting()
    { 
      shooting.enabled = false;
        Debug.Log("LockShooting");
    } 
    private void UnlockShooting() => shooting.enabled = true;

   // public void OnGunSelected() => UpdateShootingLock();
    public void UpdateShootingLock() => shooting.enabled = _loadAmmo > 0 && numOfMag > 0;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    public virtual void  Reload()
    {
        if (!PV.IsMine) return;

        if (!CanReload()) return;
        anim.SetTrigger("Reload");
      // SetLoadAmmo();
       // RefillAmmo();
             
   
    }
    public virtual void  AddAmmo()
    {
        Debug.Log("AddAmmo");
    }

    private void RefillAmmo() 
    {
        LoadAmmo = magSize;
        UnlockShooting();
    } 

    public bool CanReload()
    {
        if (numOfMag > 0)
        {          
            return true;
        }
        return false;
    }
    public bool isShooting()
    {
        if (LoadAmmo > 0)
        {
            return true;
        }
        return false;
    }

    public void SetLoadAmmo()
    {
        Debug.Log("setloadammo");

        if (numOfMag > magSize)
        {
            numOfMag = numOfMag - magSize + LoadAmmo;
            LoadAmmo = magSize;
        }

        if (numOfMag == magSize)
        {
            numOfMag = numOfMag - magSize + LoadAmmo;
            LoadAmmo = magSize;

        }

        if (numOfMag < magSize)
        {

            if (numOfMag + LoadAmmo < magSize)
            {
                LoadAmmo += numOfMag;
                numOfMag = 0;
            }
            else if (numOfMag + LoadAmmo == magSize)
            {
                LoadAmmo = magSize;
                numOfMag = 0;
            }
            else
            {
                numOfMag = numOfMag + LoadAmmo - magSize;
                LoadAmmo = magSize;
                
            }
          
           
        }
        ammoText.UpdateGunAmmo();
    }

    public virtual void IncreasenNumofMag()
    {
        Debug.Log(transform.name + "IncGunAAMo");
        numOfMag += magSize;
        ammoText.UpdateGunAmmo();
    }

    public void PlayReloadPart1Sound() => reloadSounds[0].Play();
    public void PlayReloadPart2Sound() => reloadSounds[1].Play();
    public void PlayReloadPart3Sound() => reloadSounds[2].Play();
    public void PlayReloadPart4Sound() => reloadSounds[3].Play();
    public void PlayReloadPart5Sound() => reloadSounds[4].Play(); 

}
