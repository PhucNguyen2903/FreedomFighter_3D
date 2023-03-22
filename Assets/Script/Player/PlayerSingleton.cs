using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    public PlayerUi playerUi;
    public Camera mainCamera;
    public Transform PlayerFoot;
    public PlayerHealth PlayerHealth;
}
