using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0}
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX;
    public float sensitivityY;
    public float minimumX;
    public float maximumX;
    public float minimumY;
    public float maximumY;
    float rotationY;
    float rotationX;

    void FixedUpdate()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }
}
