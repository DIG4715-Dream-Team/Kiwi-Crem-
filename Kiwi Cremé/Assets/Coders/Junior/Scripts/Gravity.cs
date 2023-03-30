using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravityScale = 1.0f;
    [SerializeField]
    public static float globalgravity = -9.81f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalgravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
