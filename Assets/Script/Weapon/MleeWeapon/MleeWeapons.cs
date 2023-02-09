using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MleeWeapons : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public int damage;
    public float explosionRadius;
    public float timeDelay;
    public float time;
    private List<Health> oldVictimsM = new List<Health>();
    [SerializeField] private AudioSource MleeSound;
    [SerializeField] private ParticleSystem MleeEffect;
    [SerializeField] private ParticleSystem MleeEffectRightMouse;
    [SerializeField] private Transform MleeEffectPos;



    private void Awake()
    {
        MleeEffect.gameObject.SetActive(true);
    }
    private void Update()
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
          anim.Play("OneClickMouse", layer: -1, normalizedTime: 0);
          PlayMusic();
          BlowObject();
          EffecTriggerOneClick();
          time = 0;
        }      
    }

    private void TwoClickMouse()
    {
        time += Time.deltaTime;
        if (time < timeDelay) return;

        if (Input.GetMouseButtonDown(1))
        {
            anim.Play("TwoClickMouse", layer: -1, normalizedTime: 0);
            PlayMusic();
            EffecTriggerTwoClick();
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
            //AddForceToObject(affectedObjects[i]); 
        }
    }

    private void DeliverDamage(Collider victim)
    {
        Health health = victim.GetComponentInParent<Health>();

        if (health != null && !oldVictimsM.Contains(health))
        {
            health.TakeDamage(damage);
            oldVictimsM.Add(health);
        }
    }

    private void PlayMusic()
    {
        MleeSound.Play();
    }

    private void EffecTriggerOneClick()
    {
        Instantiate(MleeEffect,MleeEffectPos.transform.position, MleeEffectPos.transform.rotation);
    }
    private void EffecTriggerTwoClick()
    {
        Instantiate(MleeEffectRightMouse, MleeEffectPos.transform.position, MleeEffectPos.transform.rotation);
    }

    //private void WaitingTime()
    //{
              
    //}



}
