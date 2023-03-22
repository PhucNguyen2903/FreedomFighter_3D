using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float movingSpeed;
    public float playerVelocity;
   
    
    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
        //float hinput = Input.GetAxis("Horizontal");
        //float vinput = Input.GetAxis("Vertical");

        //Vector3 direction = transform.right * hinput + transform.forward * vinput;
        //characterController.SimpleMove(direction * movingSpeed);
        
    }

    public void MoingBYKey()
    {
        float hinput = Input.GetAxis("Horizontal");
        float vinput = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * hinput + transform.forward * vinput;
        characterController.SimpleMove(direction * movingSpeed);
    }
}
