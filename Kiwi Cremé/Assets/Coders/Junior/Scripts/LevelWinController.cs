using UnityEngine;

public class LevelWinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hasHellPearl == true)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().UpdateObjective("Hell");
            }

            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hasHeavenPearl == true)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().UpdateObjective("Heaven");
            }

            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hasPurgatoryPearl == true)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().UpdateObjective("Purgatory");
            }

            
        }
    }
}
