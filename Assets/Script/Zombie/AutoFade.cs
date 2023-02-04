using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFade : MonoBehaviour
{
    public float visibleDuration;
    public float fadingDuration;
    public CanvasGroup group;

    public Image image;
    private float startTime;

    public void Show()
    {
        startTime = Time.time;
        group.alpha = 1f;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < visibleDuration) return;

        elapsedTime -= visibleDuration;
        if (elapsedTime < fadingDuration)
        {
            //SetAlpha(1f - elapsedTime/fadingDuration);
            group.alpha = 1f - elapsedTime / fadingDuration;
            
        }
        else
        {
            Hide();
        }
    }


    private void SetAlpha(float alpha)
    {
        Color newColor = image.color;
        newColor.a = alpha;
        image.color = newColor;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        image = GetComponent<Image>();
    }

}
