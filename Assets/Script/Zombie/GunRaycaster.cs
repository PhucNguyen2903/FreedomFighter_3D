using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GunRaycaster : MonoBehaviourPun
{
    public GameObject hitMarkerPrefab;
    public GameObject hitZombiePrefab;
    public Camera aimingCamera;
    public LayerMask layerMask;
    public int damage;
    public PhotonView PV;
    GameObject effectPrefab;
    bool isShooting = false;
    public GunAmmo gunAmmo;
    public ScoreText scoreText;

    private void Awake()
    {
        effectPrefab = hitMarkerPrefab;
    }


    public void PerformRaycasting()
    {
        Ray aimingRay = new Ray(aimingCamera.transform.position, aimingCamera.transform.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, float.PositiveInfinity, layerMask))
        {
            // PV.RPC("ShowHieffect", RpcTarget.AllBufferedViaServer,hitInfo);
            ShowHieffect(hitInfo);
            DeliverDamage(hitInfo);
        }
        Debug.DrawRay(aimingCamera.transform.position, aimingRay.direction);
    }

    public void ShowHieffect(RaycastHit hitInfo)
    {
        HitSurface hitSurface = hitInfo.collider.GetComponent<HitSurface>();
        if (hitSurface != null)
        {
            if (hitSurface.surfaceType == HitSurfaceType.Dirt)
            {
                effectPrefab = hitMarkerPrefab;
            }
            if (hitSurface.surfaceType == HitSurfaceType.Blood)
            {
                effectPrefab = hitZombiePrefab;
            }

            // effectPrefab = HitEffectManager.Instance.GetEffectPrefab(hitSurface.surfaceType);
        }



        if (PV.IsMine)
        {
            isShooting = gunAmmo.isShooting();
        }

        if (effectPrefab != null && isShooting)
        {
            Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
            if (PV.IsMine)
            {
                PV.RPC("Rpc_Prefab", RpcTarget.All, hitInfo.point, effectRotation);
            }
        }
    }

    [PunRPC]
    public void Rpc_Prefab(Vector3 hitPos, Quaternion hitRos)
    {
        Instantiate(effectPrefab, hitPos, hitRos);
    }

    private void DeliverDamage(RaycastHit hitInfo)
    {
        Health health = hitInfo.collider.GetComponentInParent<Health>();
        PhotonView PVzombieTakedame = hitInfo.collider.GetComponentInParent<PhotonView>();
        if (PVzombieTakedame != null && health != null && isShooting)
        {
               if (health.HealthPoint < 1) return;
            PVzombieTakedame.RPC("TakeDamage", RpcTarget.All, damage);
            scoreText.Score += damage;

            // health.TakeDamage(damage);
        }
    }

}
