using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRoomProfile : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected RoomProfile roomProfile;

    public virtual void SetRoomProfile(RoomProfile roomProfile)
    {
        this.roomProfile = roomProfile;
        this.roomName.text = this.roomProfile.name;
    }


    public virtual void Onclick()
    {
        Debug.Log("On Click: " + this.roomProfile.name);
        PhotonRoom.instance.findroomInput.text = this.roomProfile.name;
    }
   

}
