using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); 
        float zMove = Input.GetAxisRaw("Vertical");
   
        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;
    }
}
