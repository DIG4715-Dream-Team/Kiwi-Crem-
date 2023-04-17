using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamController : MonoBehaviour
{
    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;
    public float mouseSensitivity = 1.0f;
    public float gamepadSensitivity = 1.0f;
    float rotationY;
    float rotationX;

    private InputAction cameraGamepadAction;
    private InputAction cameraMouseAction;

    private const float GAMEPAD_MAX_VALUE = 1;


    private void OnEnable()
    {
        cameraGamepadAction = new InputAction("CameraGamepad", InputActionType.Value, "<Gamepad>/rightStick");
        cameraGamepadAction.Enable();

        cameraMouseAction = new InputAction("CameraMouse", InputActionType.Value, "<Mouse>/delta");
        cameraMouseAction.Enable();
    }

    private void OnDisable()
    {
        cameraGamepadAction.Disable();
        cameraMouseAction.Disable();
    }

    void FixedUpdate()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        // Get the camera input from the gamepad
        Vector2 cameraInput = cameraGamepadAction.ReadValue<Vector2>();

        // Get the camera input from the mouse and add it to the gamepad input
        cameraInput += cameraMouseAction.ReadValue<Vector2>() * mouseSensitivity;

        float mouseX = cameraInput.x;
        float mouseY = cameraInput.y;

        // Calculate the new camera rotation
        rotationY += mouseY * gamepadSensitivity;
        rotationX += mouseX * gamepadSensitivity;

        // Clamp the rotation values
        rotationY = Mathf.Clamp(rotationY, -45, 90);

        // Apply the new rotation to the camera
        transform.localRotation = Quaternion.Euler(-rotationY, 0, 0);

        // Rotate the parent player object around the y-axis
        transform.parent.Rotate(Vector3.up, mouseX);
    }
}