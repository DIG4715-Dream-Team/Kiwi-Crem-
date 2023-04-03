using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflected : MonoBehaviour
{
    public LayerMask collisionMask;
    private float speed = 5;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f,collisionMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            // Deactivate all scripts and components on the target object
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
