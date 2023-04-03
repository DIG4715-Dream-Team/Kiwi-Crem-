using UnityEngine;
using UnityEngine.SceneManagement;

public class PearlController : MonoBehaviour
{
    private Scene currentScene;
    private string scene;
    private GameObject player;
    private PlayerController Player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.GetComponent<PlayerController>();
        currentScene = SceneManager.GetActiveScene();
        scene = currentScene.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && scene == "Hell")
            {
                Player.UpdatePearl("Hell");
            }
        }
    }
}
