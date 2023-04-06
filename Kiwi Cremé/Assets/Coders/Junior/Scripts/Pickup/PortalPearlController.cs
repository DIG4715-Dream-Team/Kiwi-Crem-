using UnityEngine;

public class PortalPearlController : MonoBehaviour
{
    private GameObject player;
    private PlayerController Player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("EntryPearl"))
            {
                Player.UpdatePearlAmount("Entry", 1);
                Destroy(gameObject);
            }
            if (gameObject.CompareTag("ExitPearl"))
            {
                Player.UpdatePearlAmount("Exit", 1);
                Destroy(gameObject);
            }
        }
    }
}
