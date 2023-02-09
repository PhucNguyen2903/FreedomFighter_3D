using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Sniper : Shooting
{
    public Animator anim;
    public AudioSource bolt1Sound;
    public AudioSource bolt2Sound;
    public AudioSource zoomSound;
    public AudioSource fireSound;

    private bool isScoped = false;
    [SerializeField] protected GameObject scopeOverlay;
    [SerializeField] protected GameObject SniperCamera;
    [SerializeField] protected Camera MainCamera;

    public UnityEvent onFire;
    private float lastShot;
    private float interval;
    public int rpm;
    private void Start()
    {
        interval = 60f / rpm;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Zoom();
        }
        if (Input.GetMouseButtonDown(0))
        {
            updateFiring();
        }
        
    }

    //public void PlayRetrieveSound()
    //{
    //}
    public void HideCollimator() { }
    public void PlayBoltPart2Sound() => bolt2Sound.Play();
    public void PlayBoltPart1Sound() => bolt1Sound.Play();


    public void PlayFireSound() => fireSound.Play();
    private void Fire()
    {
        anim.Play("Fire", layer: -1, normalizedTime: 0);
        onFire.Invoke();
    }

    private void updateFiring()
    {
        if (Time.time - lastShot >= interval)
        {
            Fire();
            lastShot = Time.time;
        }
    }

    private void Zoom()
    {
        anim.Play("Zoom", layer: -1, normalizedTime: 0);
        zoomSound.Play();
        isScoped = !isScoped;
        scopeOverlay.SetActive(isScoped);
        SniperCamera.SetActive(!isScoped);
        MainCamera.fieldOfView = 15f;
        DoubleMouse();
    }

    private void DoubleMouse()
    {
        if (isScoped) return;
         anim.SetTrigger("Idle");
         MainCamera.fieldOfView = 60f;
       
        //if (!isScoped)
        //{
        //    anim.SetTrigger("Idle");
        //    MainCamera.fieldOfView = 60f;
        //}

    }

    
}
