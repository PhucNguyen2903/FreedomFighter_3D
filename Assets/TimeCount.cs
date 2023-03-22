using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeCount;
    int calTimeMinus;
    int calTimeSecond;

    private void Update()
    {
        timeCount += Time.deltaTime;
        UpdateTime();
    }

    void CalTime()
    {
        

        if (timeCount < 59)
        {
            calTimeMinus = 0;
            calTimeSecond = (int) timeCount;
        }
        else
        {           
            calTimeMinus = (int) (timeCount / 60);
            calTimeSecond = (int)(timeCount % 60);
        }
    }

    void UpdateTime()
    {
        CalTime();
        timeText.text = calTimeMinus + " : " + calTimeSecond;
        timeText.text = string.Format("{0:00} : {1:00}", calTimeMinus, calTimeSecond);


    }
}
