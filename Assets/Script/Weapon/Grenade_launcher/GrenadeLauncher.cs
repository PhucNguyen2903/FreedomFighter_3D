using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GrenadeLauncher : Shooting
{
    private const int LeftMouseButton = 0;
    public GameObject bulletPrefab;
    public Transform firingPos;
    public float bulletSpeed;
    public AudioSource shootingSound;
    public Animator anim;
    public Transform aimPos;
    public PhotonView PV;
    public GunAmmo gunAmmo;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(LeftMouseButton))
        //{
        //    ShootingBullet();
        //}
    }

    public void GrenadeButton()
    {
        if (Input.GetMouseButton(LeftMouseButton))
        {
            ShootingBullet();
        }
    }

    private void ShootingBullet() 
    {
        bool isShooting = gunAmmo.isShooting();
        if (!isShooting) return;
        anim.SetTrigger("Shoot");
    } 
    public void PlayFireSound() => shootingSound.Play();
    public void AddProjectile()
    {
        //GameObject bullet = Instantiate(bulletPrefab, firingPos.position, firingPos.rotation);
        //bullet.SetActive(true);
        //bullet.GetComponent<PhotonView>().ViewID = Random.Range(1001,9999);
        //bullet.transform.LookAt(aimPos);
        //bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        PV.RPC("RPC_AddProjectile",RpcTarget.All);
    }


    [PunRPC]
    void RPC_AddProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPos.position, firingPos.rotation);
        bullet.SetActive(true);
        bullet.GetComponent<PhotonView>().ViewID = Random.Range(1001, 9999);
        bullet.transform.LookAt(aimPos);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
