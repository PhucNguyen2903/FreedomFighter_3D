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
    [SerializeField] Transform PlayerTransform;
    [SerializeField] PlayerHealth playerhealth;
    public bool isground;
    public float heightTakedame;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        isground = characterController.isGrounded;
    }

    // Update is called once per frame
    void Update()
    {




    }

    public void KeyJump()
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
        if (_jumPressed && _groundPlayer)
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

    public void FallDown()
    {
        //if (characterController.isGrounded) return;
        //float maxHeight = 4.35f;
        //float height = PlayerTransform.transform.position.y;
        //if (height < maxHeight) return;
        //int Dame = (int)(height - maxHeight) * 10;
        //injuredFall(Dame, height);

    }

    void injuredFall(int Dame, float height)
    {
        //heightTakedame = height;
        if (!canInjured(height)) return;
        playerhealth.TakeDamage(Dame);
        heightTakedame = 0;
    }

    bool canInjured(float height)
    {
        if (height <= heightTakedame)
        {
            return false;
        }
        return true;
    }
}
