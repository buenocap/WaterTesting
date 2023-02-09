/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float SensitivityX = 200f;
    [SerializeField] float SensitivityY = 200f;
    Camera cam;
    float verticalRotation;

    public VariableJoystick variableJoystick;
    private Vector2 playerInput;

    [SerializeField] bool _isMobileDevice = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (_isMobileDevice)
        {
            //getting the input of joystick
            playerInput = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical) * Time.deltaTime;
    
        }
        else
        {
            //getting the input of mouse delta
            playerInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * Time.deltaTime;
     
        }

        //creating another float variable to clamp it later
        verticalRotation -= playerInput.y * SensitivityY;

        //clamping verticalRotation
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        //setting the localRotation of the camera
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //horizontal rotation of player along with camera.
        transform.Rotate(Vector3.up * playerInput.x * SensitivityX);
    }
}