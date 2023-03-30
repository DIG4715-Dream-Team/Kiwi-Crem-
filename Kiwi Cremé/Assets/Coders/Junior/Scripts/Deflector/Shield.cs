using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float spawnDistance = 3f;
    [SerializeField] private GameObject Deflector;
    public bool canspawn = true;

    void Update()
    {
        if (canspawn == true && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(Deflector, player.position + player.forward * spawnDistance, player.rotation);
        }
    }
}
