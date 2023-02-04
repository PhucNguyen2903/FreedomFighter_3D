using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerUi playerUi;
    public Camera mainCamera;
    public Transform PlayerFoot;
    public PlayerHealth PlayerHealth;
}
