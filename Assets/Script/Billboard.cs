using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Player.Instance.mainCamera;
        LookTowardCamera();
    }
    private void Update()
    {
        LookTowardCamera();
    }

    private void LookTowardCamera()
    {
        transform.forward = - mainCamera.transform.forward;
    }
}
