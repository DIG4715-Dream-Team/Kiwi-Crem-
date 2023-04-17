using System.Xml;
using UnityEngine;
using UnityEngine.Rendering;

public class AngelProjectileController : MonoBehaviour
{
    public GameObject Projectile;
    public float launchVelocity = 700f;

    public AudioSource audioSource;
    public AudioClip FireBall_Sound;

    private bool colliding = false;

    public void FireProjectile()
    {
        GameObject ball = Instantiate(Projectile, transform.position, transform.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * launchVelocity, ForceMode.Impulse);

        audioSource.PlayOneShot(FireBall_Sound);

        ball.tag = "Projectile";
        
        if (ball.GetComponent<Deflected>().colliding == false)
        {
            Destroy(ball, 5f);
        }
        else if (ball.GetComponent<Deflected>().colliding == true)
        {
            Destroy(ball, 10f);
        }
    }
}