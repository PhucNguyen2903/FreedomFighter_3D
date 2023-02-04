using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyExplosion : MonoBehaviour
{
    public int lifetime = 5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

   
}
