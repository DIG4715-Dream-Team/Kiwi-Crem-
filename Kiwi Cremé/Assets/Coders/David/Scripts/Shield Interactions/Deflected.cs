using UnityEngine;

public class Deflected : MonoBehaviour
{
    public LayerMask collisionMask;
    private GameObject player;
    private PlayerController Player;

    private bool colliding = false;

    public bool Colliding { get => colliding; set => colliding = value; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.HealthManagement(-5);
        }
        if (collision.gameObject.CompareTag("Turret"))
        {
            // Deactivate all scripts and components on the target object
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Angel"))
        {
            // Deactivate all scripts and components on the target object
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            Colliding = true;
        }
    }
}