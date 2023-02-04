using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoText : MonoBehaviour
{
    public TextMeshProUGUI loadAmmoText;
    public GunAmmo gunAmmo;
    // Start is called before the first frame update
    private void Start()
    {
        gunAmmo.loadAmmoChanged.AddListener(UpdateGunAmmo);
        //yield return new WaitForEndOfFrame();
        UpdateGunAmmo();
    }

    // Update is called once per frame
    public void UpdateGunAmmo() => loadAmmoText.text = gunAmmo.LoadAmmo.ToString();
    
}
