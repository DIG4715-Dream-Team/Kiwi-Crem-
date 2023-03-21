using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflected : MonoBehaviour
{

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
    }

    void OnCollisionEnter(Collision collision)
        {
            Vector3 v = Vector3.Reflect(transform.up, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(90, rot, 0);
        }
}
