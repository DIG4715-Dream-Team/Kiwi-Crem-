using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI middleText;
    public TextMeshProUGUI MiddleText { get; private set; }
    [SerializeField]
    public string currentScene { get; private set; }
    [SerializeField]
    public Scene activeScene { get; private set; }
    private bool isPaused;

    [SerializeField]
    private GameObject mainMenu;
    [HideInInspector]
    public bool inMainMenu;
    [SerializeField]
    private GameObject aboutMenu;
    private bool inAboutMenu;
    [SerializeField]
    private GameObject controlMenu;
    private bool inControlMenu;
    [SerializeField]
    private GameObject creditMenu;
    private bool inCreditMenu;
    [SerializeField]
    private GameObject pauseMenu;
    private bool inPauseMenu;
    [SerializeField]
    private GameObject endMenu;
    public GameObject EndMenu { get; private set; }

    private GameObject player;
    private PlayerController Player;

    void Start()
    {
        EndMenu = endMenu;
        MiddleText = middleText;
        SceneCheck();
        if (currentScene != "Tiny_Shell_MainMenu")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Player = player.GetComponent<PlayerController>();
        }
        HUDPreset();
    }

    void Update()
    {
        PauseLogic();
    }

    private void HUDPreset()
    {
        if (currentScene == "Tiny_Shell_MainMenu")
        {
            MiddleText.text = "Tiny Shell\nGet to the shore as fast as possible";
            mainMenu.SetActive(true);
            aboutMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
            pauseMenu.SetActive(false);
            endMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(false);
            aboutMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
            pauseMenu.SetActive(false);
            endMenu.SetActive(false);
        }
    }

    private void SceneCheck()
    {
        activeScene = SceneManager.GetActiveScene();
        currentScene = activeScene.name;
        if (currentScene == "Tiny_Shell_MainMenu")
        {
            inMainMenu = true;
        }
    }

    private void PauseLogic()
    {
        if (currentScene != "Tiny_Shell_MainMenu")
        {
            if (Player.GameOver == false)
            {
                if (currentScene != "Tiny_Beach_MainMenu" && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    Time.timeScale = 0;
                    isPaused = true;
                    Cursor.lockState = CursorLockMode.None;
                }

                if (isPaused == true & inPauseMenu == false)
                {
                    pauseMenu.SetActive(true);
                    inPauseMenu = true;
                    MiddleText.text = "Game Is Paused";
                }

                if (Time.timeScale == 1)
                {
                    pauseMenu.SetActive(false);
                    inPauseMenu = false;
                    MiddleText.text = "";
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseLogic();
        MiddleText.text = "";
    }

    private void CheckActivity()
    {
        if (inMainMenu == true)
        {
            mainMenu.SetActive(false);
        }
        else if (inPauseMenu == true)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        inMainMenu = false;
    }

    public void About()
    {
        CheckActivity();
        aboutMenu.SetActive(true);
        inAboutMenu = true;
        MiddleText.text = "Options";

    }

    public void Controls()
    {
        controlMenu.SetActive(true);
        inControlMenu = true;
        pauseMenu.SetActive(false);
        aboutMenu.SetActive(false);
        CheckActivity();
        MiddleText.text = "Controls\nW,A,S, and D to move\nF to activate ability";
    }

    public void Credit()
    {
        creditMenu.SetActive(true);
        inCreditMenu = true;
        pauseMenu.SetActive(false);
        aboutMenu.SetActive(false);
        CheckActivity();
        MiddleText.text = "Credit\nCameron Welsh - Art | Pablo Sarria - Tech | Junior Rojas - Code";
    }

    public void Back()
    {
        if (inMainMenu == true && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inMainMenu == false && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }

        if (inMainMenu == true && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inMainMenu == false && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }

        if (inMainMenu == true && inAboutMenu == true)
        {
            aboutMenu.SetActive(false);
            inAboutMenu = false;
            mainMenu.SetActive(true);
            MiddleText.text = "";
            return;
        }
        else if (inMainMenu == false && inAboutMenu == true)
        {
            aboutMenu.SetActive(false);
            inAboutMenu = false;
            pauseMenu.SetActive(true);
            MiddleText.text = "Game Paused";
            return;
        }

        if (inMainMenu == true && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inMainMenu == false && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }

        if (inMainMenu == true && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inMainMenu == false && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }



        if (inPauseMenu == true && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inPauseMenu == false && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }

        if (inPauseMenu == true && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inPauseMenu == false && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }

        if (inPauseMenu == true && inAboutMenu == true)
        {
            aboutMenu.SetActive(false);
            inAboutMenu = false;
            mainMenu.SetActive(true);
            MiddleText.text = "";
            return;
        }
        else if (inPauseMenu == false && inAboutMenu == true)
        {
            aboutMenu.SetActive(false);
            inAboutMenu = false;
            pauseMenu.SetActive(true);
            MiddleText.text = "Game Paused";
            return;
        }

        if (inPauseMenu == true && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inPauseMenu == false && inCreditMenu == true)
        {
            creditMenu.SetActive(false);
            inCreditMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }

        if (inPauseMenu == true && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
        else if (inPauseMenu == false && inControlMenu == true)
        {
            controlMenu.SetActive(false);
            inControlMenu = false;
            aboutMenu.SetActive(true);
            MiddleText.text = "Options";
            return;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void QuitToMain(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        inMainMenu = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
