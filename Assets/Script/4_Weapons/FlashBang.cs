using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    [SerializeField] ParticleSystem Flashexplosionprefab;
    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(Flashexplosionprefab, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
