using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MleeEffectTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem MleeEffect;
    [SerializeField] private Transform MleeEffectPos;
    

    private void Update()
    {
        OneCLickMouse();
        TwoClickMouse();
    }
    private void OneCLickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(MleeEffect, transform.position, transform.rotation);   
        }
    }

    private void TwoClickMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(MleeEffect, MleeEffectPos.transform.position,MleeEffectPos.transform.rotation);
        }
    }
}
