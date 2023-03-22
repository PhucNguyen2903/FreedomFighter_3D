using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MleeWeapons : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public int damage;
    public float explosionRadius;
    public float timeDelay;
    public float time;
    private List<Health> oldVictimsM = new List<Health>();
    [SerializeField] private PhotonView PV;
    [SerializeField] private AudioSource MleeSound;
    [SerializeField] private ParticleSystem MleeEffect;
    [SerializeField] private ParticleSystem MleeEffectRightMouse;
    [SerializeField] private Transform MleeEffectPos;
    [SerializeField] private ScoreText scoreText;



    private void Awake()
    {
        MleeEffect.gameObject.SetActive(true);
    }
    private void Update()
    {
        //OneCLickMouse();
        //TwoClickMouse();
    }

    public void MleeButton()
    {
        OneCLickMouse();
        TwoClickMouse();
    }



    private void OneCLickMouse()
    {
        time += Time.deltaTime;

        if (time < timeDelay) return;

        if (Input.GetMouseButtonDown(0))
        {
            // anim.Play("OneClickMouse", layer: -1, normalizedTime: 0);
            // EffecTriggerOneClick();
            PlayMusic();
            BlowObject();
            PV.RPC("CallAnimOne", RpcTarget.All);
            PV.RPC("EffecTriggerOneClick", RpcTarget.All);
            time = 0;
        }
    }

    private void TwoClickMouse()
    {
        time += Time.deltaTime;
        if (time < timeDelay) return;

        if (Input.GetMouseButtonDown(1))
        {
            // anim.Play("TwoClickMouse", layer: -1, normalizedTime: 0);
            // EffecTriggerTwoClick();
            PV.RPC("CallAnimTwo", RpcTarget.All);
            PlayMusic();
            PV.RPC("EffecTriggerTwoClick", RpcTarget.All);
            BlowObject();
            time = 0;
        }
    }
    private void BlowObject()
    {
        oldVictimsM.Clear();
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            DeliverDamage(affectedObjects[i]);
        }
    }

    //private void DeliverDamage(Collider victim)
    //{
    //    Health health = victim.GetComponentInParent<Health>();

    //    if (health != null && !oldVictimsM.Contains(health))
    //    {
    //        health.TakeDamage(damage);
    //        oldVictimsM.Add(health);
    //    }
    //}
    private void DeliverDamage(Collider victim)
    {
        Health health = victim.GetComponentInParent<Health>();
        PhotonView PVzombieTakedame = victim.GetComponentInParent<PhotonView>();

        if (health != null && !oldVictimsM.Contains(health) && PVzombieTakedame != null)
        {
            if (health.HealthPoint < 1) return;

            PVzombieTakedame.RPC("TakeDamage", RpcTarget.All, damage);
            scoreText.Score += damage;
            oldVictimsM.Add(health);
        }
    }

    private void PlayMusic()
    {
        MleeSound.Play();
    }

    [PunRPC]
    private void EffecTriggerOneClick()
    {
        Instantiate(MleeEffect, MleeEffectPos.transform.position, MleeEffectPos.transform.rotation);
    }
    [PunRPC]
    private void EffecTriggerTwoClick()
    {
        Instantiate(MleeEffectRightMouse, MleeEffectPos.transform.position, MleeEffectPos.transform.rotation);
    }

    [PunRPC]
    public void CallAnimTwo()
    {
        anim.Play("TwoClickMouse", layer: -1, normalizedTime: 0);
    }
    [PunRPC]
    public void CallAnimOne()
    {
        anim.Play("OneClickMouse", layer: -1, normalizedTime: 0);
    }

}
