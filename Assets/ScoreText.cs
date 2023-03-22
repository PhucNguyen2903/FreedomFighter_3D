using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int Score;

    private void Update()
    {
        UpdateSocre();
    }

     void UpdateSocre()
    {
        scoreText.text = "Score: " + Score;
    }
}
