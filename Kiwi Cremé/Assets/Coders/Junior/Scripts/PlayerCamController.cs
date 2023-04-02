using System.Collections;
using UnityEngine;


public class PlayerCamController : MonoBehaviour
{
    public float sensitivityX = 2;
    public float sensitivityY = 2;
    float rotationY;
    float rotationX;

    [SerializeField]
    private GameObject player;

    void FixedUpdate()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Calculate the new camera rotation
        rotationY += mouseY;
        rotationX += mouseX;

        // Clamp the rotation values
        rotationY = Mathf.Clamp(rotationY, -45, 45);

        // Apply the new rotation to the camera
        transform.localRotation = Quaternion.Euler(-rotationY, 0, 0);

        // Rotate the player object around the y-axis
        player.transform.Rotate(Vector3.up, mouseX);
    }
}