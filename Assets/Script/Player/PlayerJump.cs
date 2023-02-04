using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{

    private CharacterController characterController;

    private bool _groundPlayer;
    private Vector3 _playerVelocity;

    [SerializeField] private float jumpHeight = 5.0f;
    private bool _jumPressed = false;
    private float _gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            MovementJump();
        }
            
        
    }

    void MovementJump()
    {
        _groundPlayer = characterController.isGrounded;
        if (_groundPlayer) 
        {
            _playerVelocity.y = 0.0f;
            OnJump();
        }
        if(_jumPressed && _groundPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -0.1f * _gravityValue);
            _jumPressed = false;
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        characterController.Move(_playerVelocity * Time.deltaTime);
    }
    void OnJump()
    {
        if (characterController.velocity.y == 0)
        {
            _jumPressed = true;
        }
          
    }
}
