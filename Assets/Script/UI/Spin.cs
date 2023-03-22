using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spin : MonoBehaviour
{
   

    public float numberOfGift = 8;
    public float timeRotate;
    public float numberCircleRotate;

    private const float circle = 360.0f;
    private float angleOfoneGift;

    public Transform parent;
    private float currentTime;

    public AnimationCurve curve;

    public PlayerInfo playerInfo;
    public int indexGiftRandom;

    private void Start()
    {
        angleOfoneGift = circle / numberOfGift;
        SetPosData();
    }


    IEnumerator RotateWheel()
    {
        float startAngle = transform.eulerAngles.z;
        currentTime = 0;

        indexGiftRandom =  (int)Random.Range(1, numberOfGift);
        float anglewant = (numberCircleRotate * circle) + angleOfoneGift * indexGiftRandom - startAngle;
        Debug.Log(indexGiftRandom);

        while (currentTime < timeRotate)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;

            float angleCurrent = anglewant * curve.Evaluate(currentTime / timeRotate);
            this.transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }
    }

   [ContextMenu("Rotate")]
    public void RotateNow()
    {
        StartCoroutine(RotateWheel());
        
    }


    private void SetPosData()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).eulerAngles = new Vector3(0, 0, -circle / numberOfGift * i);
            parent.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            

        }
    }
}
