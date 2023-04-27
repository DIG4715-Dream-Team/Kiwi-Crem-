using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{
    public Vector3 targetPosition; // the position where the player will be impulse
    public float impulseMagnitude; // the magnitude of the impulse force
    public float gravityDisableDuration; // the duration for which the player's gravity will be disabled

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 impulseDirection = (targetPosition - playerRb.position).normalized;
                playerRb.AddForce(impulseDirection * impulseMagnitude, ForceMode.VelocityChange);
                StartCoroutine(DisableGravity(playerRb));
            }
        }
    }

    IEnumerator DisableGravity(Rigidbody rb)
    {
        rb.useGravity = false;
        yield return new WaitForSeconds(gravityDisableDuration);
        rb.useGravity = true;
    }
}
