using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public float launchVelocity = 700f;

    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;

    public AudioSource audioSource;
    public AudioClip FireBall_Sound;

    void Update()
    {
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;

            GameObject ball = Instantiate(Projectile, transform.position, transform.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * launchVelocity, ForceMode.Impulse);

            audioSource.PlayOneShot(FireBall_Sound);

            ball.tag = "Projectile";

            Destroy(ball, 4f);
        }
    }
}
