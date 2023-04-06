using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    [SerializeField] private float spawnDistance = 3f;
    [SerializeField] private GameObject Deflector;
    public bool canspawn = true;
    private float cooldown = 5f;

    void Update()
    {
        if (canspawn == true && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(Deflector, player.position + player.forward * spawnDistance, cam.rotation);
            canspawn = false;
            StartCoroutine(SpawnCooldown());
        }
    }
    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canspawn = true;
    }
}
