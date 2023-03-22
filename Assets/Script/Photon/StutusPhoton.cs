using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class StutusPhoton : MonoBehaviour
{
    private string photonstatus;
    public TextMeshProUGUI textStatus;

    public static StutusPhoton instance;

    private void Awake()
    {
        StutusPhoton.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        this.photonstatus = PhotonNetwork.NetworkClientState.ToString();
        this.textStatus.text = this.photonstatus;
        
    }
}
