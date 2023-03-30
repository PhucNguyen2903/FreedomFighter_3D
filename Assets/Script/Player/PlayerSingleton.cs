using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    public PlayerUi playerUi;
    public Camera mainCamera;
    public Transform PlayerFoot;
    public PlayerHealth PlayerHealth;

}
