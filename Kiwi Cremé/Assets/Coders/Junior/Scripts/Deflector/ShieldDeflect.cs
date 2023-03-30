using UnityEngine;

public class ShieldDeflect : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other != null && gameObject.CompareTag(""))
        {
            float rotDiff = -Quaternion.Angle(transform.rotation, other.transform.rotation);
            rotDiff += 180;
            Vector3 velocity = other.rigidbody.velocity;
            other.transform.Rotate(Vector3.up, rotDiff);
            other.rigidbody.velocity = velocity;
        }
    }
}
