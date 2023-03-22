using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINameProfile : MonoBehaviour
{
     public TextMeshProUGUI PlayerNameInfo;

    public virtual void Onclick()
    {
        string playername = PlayerNameInfo.text;
        PhotonRoom.instance.playerNameChatInput.text = playername;
    }
}
