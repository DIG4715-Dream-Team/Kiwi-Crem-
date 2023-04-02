using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public float launchVelocity = 700f;
    
    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;

    void Update()
    {
        time += Time.deltaTime;
 
        if (time >= interpolationPeriod) 
        {
            time = time - interpolationPeriod;

            GameObject ball = Instantiate(Projectile, transform.position, transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity,0));

            Destroy(ball, 2f);
        }
    }
}
