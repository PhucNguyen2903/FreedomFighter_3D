using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeCount;
    public int calTimeMinus;
    public int calTimeSecond;
    bool timeStopstatus = false;

    private void Update()
    {
        TimeRun();
    }
    
    void TimeRun()
    {
        if (TimeStop()) return;
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

    public void TimeStopStatus( bool timeStopValue)
    {
        if (timeStopstatus != timeStopValue)
        {
            timeStopstatus = timeStopValue;
        }
    }

    public bool TimeStop()
    {
        return timeStopstatus;
    }

}
