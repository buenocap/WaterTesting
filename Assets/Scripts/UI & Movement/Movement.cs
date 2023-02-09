/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    Vector3 direction;
    CharacterController controller;
    private bool _useGravity;
    private float _gravityVal = -8f;

    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private float walkSpeed = 10f;

    public VariableJoystick variableJoystick;
    public FixedButton jumpButton;

    [SerializeField] bool _isMobileDevice = true;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        _useGravity = !controller.isGrounded;
        //stop downward velocity when grounded
        if (!_useGravity && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if (_isMobileDevice)
        {
            //direction of movement based on joystick position
            direction = (transform.right * variableJoystick.Horizontal + transform.forward * variableJoystick.Vertical).normalized * walkSpeed;
        }
        else
        {
            //determines direction to move horizontally and vertically (based on WASD or arrow keys)
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            //direction of movement
            direction = (transform.right * input.x + transform.forward * input.z).normalized * walkSpeed;
        }


        velocity.x = direction.x;
        velocity.z = direction.z;

        //If player is grounded and "jumps", set initial velocity.y
        if (!_useGravity && (Input.GetButtonDown("Jump") || jumpButton.Pressed))
        {
            velocity.y = _jumpSpeed;

        }

        //update velocity in accordance with gravitational acceleration
        velocity.y += _gravityVal * Time.deltaTime;

        //update player position
        controller.Move(velocity * Time.deltaTime);


    }

    public void increaseWalkSpeed(int increase) 
    {
        walkSpeed += increase;
    }
}
