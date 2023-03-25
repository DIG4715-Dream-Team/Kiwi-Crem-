using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    public float sensitivityX = 5;
    public float sensitivityY = 5;
    float rotationY;
    float rotationX;

    [SerializeField]
    private GameObject player;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, -45, 45);
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        player.transform.localRotation = Quaternion.Euler(0, rotationX, 0);
        transform.localRotation = Quaternion.Euler(-rotationY, 0, 0);

        transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
