using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] public Animator anim;
    public int magSize;
    public AudioSource[] reloadSounds;
    public Shooting shooting;
    public UnityEvent loadAmmoChanged;

    private int _loadAmmoValue;
    private int _loadAmmo;
    private bool isReloading;

    public int LoadAmmo
    {
        get => _loadAmmo;
        
        
        set
        {
            _loadAmmo = value;
            loadAmmoChanged.Invoke();
            if (_loadAmmo <= 0)
            {
                LockShooting();
                Reload();
                
            }
            else
            {
                UnlockShooting();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       RefillAmmo();
       
    }

   

    public virtual void SingleFireAmmoCounter()
    {
        if (LoadAmmo == 0) return;
        LoadAmmo--;
    }
    public void LockShooting() => shooting.enabled = false;
    private void UnlockShooting() => shooting.enabled = true;

    public void OnGunSelected() => UpdateShootingLock();
    public void UpdateShootingLock() => shooting.enabled = _loadAmmo > 0;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    public virtual void  Reload()
    {
        anim.SetTrigger("Reload");
        RefillAmmo();      
   
    }
    public virtual void  AddAmmo()
    {
        RefillAmmo();
    }

    private void RefillAmmo() 
    {
        LoadAmmo = magSize;
        UnlockShooting();
    } 


    public void PlayReloadPart1Sound() => reloadSounds[0].Play();
    public void PlayReloadPart2Sound() => reloadSounds[1].Play();
    public void PlayReloadPart3Sound() => reloadSounds[2].Play();
    public void PlayReloadPart4Sound() => reloadSounds[3].Play();
    public void PlayReloadPart5Sound() => reloadSounds[4].Play(); 

}
