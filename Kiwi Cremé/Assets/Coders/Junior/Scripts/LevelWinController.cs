using UnityEngine;

public class LevelWinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().hasHellPearl == true)
            {
                other.gameObject.GetComponent<PlayerController>().UpdateObjective("Hell");
            }

            if (other.gameObject.GetComponent<PlayerController>().hasHeavenPearl == true)
            {
                other.gameObject.GetComponent<PlayerController>().UpdateObjective("Heaven");
            }

            if (other.gameObject.GetComponent<PlayerController>().hasPurgatoryPearl == true)
            {
                other.gameObject.GetComponent<PlayerController>().UpdateObjective("Purgatory");
            }
        }
    }
}
