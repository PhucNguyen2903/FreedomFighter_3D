using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public string nameObj;

    private void Start()
    {
        nameObj = this.gameObject.name;
        UIManager.Instnace.observerList.Add(this);
    }

    public void SecActive(bool onclick)
    {
        this.gameObject.SetActive(onclick);
    }
}
