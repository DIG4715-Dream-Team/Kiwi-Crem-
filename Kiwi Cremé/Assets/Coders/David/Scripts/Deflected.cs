using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflected : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        float force = 3;
 
        if (c.gameObject.tag == "Deflector")
        {
            Vector3 dir = c.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir*force);
        }
    }
}
