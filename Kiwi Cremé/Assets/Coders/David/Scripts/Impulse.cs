using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{
    public Vector3 targetPosition; // the position where the player will be impulse
    public float impulseMagnitude; // the magnitude of the impulse force
    public float gravityDisableDuration; // the duration for which the player's gravity will be disabled}

    private GameObject player;
    private PlayerController Player;

    private GameObject moon;
    private PlayerController Moon;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.GetComponent<PlayerController>();

        moon = GameObject.FindGameObjectWithTag("Moon");
        Moon = player.GetComponent<PlayerController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Player.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 100f, 100f) , ForceMode.Acceleration);
        }
    }
}
