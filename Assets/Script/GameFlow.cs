using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    // Start is called before the first frame update
   public void OnPlayerDied()
    {
        Time.timeScale = 0;
        print("Player died");
    }
}
