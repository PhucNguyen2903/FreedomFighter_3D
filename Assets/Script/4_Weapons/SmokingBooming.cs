using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmokingBooming : MonoBehaviour
{
    public GameObject smokingPrefab;
    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(smokingPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
       
    }
    
}
