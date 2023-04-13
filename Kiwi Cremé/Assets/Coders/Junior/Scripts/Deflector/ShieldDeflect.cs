using UnityEngine;

public class ShieldDeflect : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 initialVelocity;

    private Rigidbody rb;

    private void Start()
    {
        // Save the initial position and velocity of the projectile
        initialPosition = transform.position;
        initialVelocity = GetComponent<Rigidbody>().velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            // Get the velocity of the projectile
            Vector3 velocity = other.rigidbody.velocity;

            // Reverse the direction of the velocity
            Vector3 newVelocity = -velocity;

            // Set the velocity of the projectile to the new velocity
            other.rigidbody.velocity = newVelocity;
        }
    }
}