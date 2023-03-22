using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_Inputfield : MonoBehaviour
{
    public GameObject textMeshPro;
    public TextMeshProUGUI text;  
    private void Awake()
    {
        this.text = textMeshPro.GetComponent<TextMeshProUGUI>();
    }



}
