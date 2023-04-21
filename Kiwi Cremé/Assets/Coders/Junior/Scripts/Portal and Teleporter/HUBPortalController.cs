using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class HUBPortalController : MonoBehaviour
{
    private GameObject buttonManager;
    private ButtonManager ButtonManager;

    void Update()
    {
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager");
        ButtonManager = buttonManager.GetComponent<ButtonManager>();
        SceneCheck();
    }

    private void SceneCheck()
    {
        if (ButtonManager.currentScene == "MainMenu")
        {
            ButtonManager.inMainMenu = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "HellPortal" && other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Hell");
        }
        if (gameObject.tag == "PurgatoryPortal" && other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Purgatory");
        }
        if (gameObject.tag == "HeavenPortal" && other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Heaven");
        }
    }
}
