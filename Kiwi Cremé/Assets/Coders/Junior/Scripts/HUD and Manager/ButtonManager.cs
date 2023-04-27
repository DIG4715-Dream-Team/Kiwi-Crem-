using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI middleText;
    public TextMeshProUGUI MiddleText { get; private set; }
    [SerializeField] public string currentScene { get; private set; }
    [SerializeField] public Scene activeScene { get; private set; }
    private bool isPaused;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject background;
    [HideInInspector] public bool inMainMenu;
    [SerializeField] private GameObject aboutMenu;
    private bool inAboutMenu;
    [SerializeField]
    private GameObject controlMenu;
    private bool inControlMenu;
    [SerializeField] private GameObject creditMenu;
    private bool inCreditMenu;
    [SerializeField] private GameObject pauseMenu;
    private bool inPauseMenu;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject settingMenu;
    private bool inSettingMenu;

    public GameObject EndMenu { get; private set; }

    private GameObject player;
    private PlayerController Player;

    [SerializeField] private GameObject crosshair;

    public AudioSource[] sources;

    private GameObject nextMenu;

    private void Awake()
    {
        SceneCheck();
    }

    void Start()
    {
        EndMenu = endMenu;
        MiddleText = middleText;
        SceneCheck();
        if (currentScene != "MainMenu")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Player = player.GetComponent<PlayerController>();
            background.SetActive(false);
        }
        HUDPreset();
        sources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    void Update()
    {
        PauseLogic();
    }

    private void HUDPreset()
    {
        if (currentScene == "MainMenu")
        {
            MiddleText.text = "Place Holder (ButtonManager/HUDPreset())";
            mainMenu.SetActive(true);
            crosshair.SetActive(false);
            aboutMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
            pauseMenu.SetActive(false);
            endMenu.SetActive(false);
            settingMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(false);
            aboutMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
            pauseMenu.SetActive(false);
            endMenu.SetActive(false);
            settingMenu.SetActive(false);
        }
    }

    private void SceneCheck()
    {
        activeScene = SceneManager.GetActiveScene();
        currentScene = activeScene.name;
        if (currentScene == "MainMenu")
        {
            inMainMenu = true;
        }
    }

    private void PauseLogic()
    {
        if (currentScene != "MainMenu")
        {
            if (Player.GameOver == false)
            {
                if (currentScene != "MainMenu" && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    Time.timeScale = 0;
                    isPaused = true;
                    if (Time.timeScale == 0)
                    {
                        foreach (AudioSource audioSource in sources)
                        {
                            if (audioSource.isPlaying)
                            {
                                audioSource.Pause();
                            }
                        }
                    }
                    Cursor.lockState = CursorLockMode.None;
                    crosshair.SetActive(false);
                }

                if (isPaused == true & inPauseMenu == false)
                {
                    pauseMenu.SetActive(true);
                    inPauseMenu = true;
                    MiddleText.text = "Game Is Paused";
                    nextMenu = pauseMenu;
                }

                if (Time.timeScale == 1)
                {
                    pauseMenu.SetActive(false);
                    crosshair.SetActive(true);
                    inPauseMenu = false;
                    if (Time.timeScale == 1)
                    {
                        foreach (AudioSource audioSource in sources)
                        {
                            audioSource.UnPause();
                        }
                    }
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
        nextMenu = aboutMenu;
    }

    public void Controls()
    {
        controlMenu.SetActive(true);
        inControlMenu = true;
        pauseMenu.SetActive(false);
        aboutMenu.SetActive(false);
        CheckActivity();
        MiddleText.text = "Controls\nW,A,S, and D or the left stick on Xbox to move\nE or A on Xbox to activate temporary shield\nHold LeftShift or X button on Xbox to crouch";
    }

    public void Settings()
    {
        settingMenu.SetActive(true);
        inSettingMenu = true;
        pauseMenu.SetActive(false);
        aboutMenu.SetActive(false);
        CheckActivity();
        MiddleText.text = "Volume Controller\n\n\nMouse Sensitivity";
    }

    public void Credit()
    {
        creditMenu.SetActive(true);
        inCreditMenu = true;
        pauseMenu.SetActive(false);
        aboutMenu.SetActive(false);
        CheckActivity();
        MiddleText.text = "Credit\n Artists\n Emily Bailey\n Cameron Welsh\n Coders\n David Metellus\n Junior Rojas Vasquez\n Techs\n Cameron Anderson\n Pablo Sarria\n";
    }

    public void DeactivateAllMenus()
    {
        aboutMenu.SetActive(false);
        inAboutMenu = false;
        controlMenu.SetActive(false);
        inControlMenu = false;
        creditMenu.SetActive(false);
        inCreditMenu = false;
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
    }

    public void Back()
    {
        if (inMainMenu == true)
        {
            HUDPreset();
            return;
        }

        if (inMainMenu == false && nextMenu == aboutMenu)
        {
            DeactivateAllMenus();
            aboutMenu.SetActive(true);
            inAboutMenu = true;
            MiddleText.text = "Options";
            nextMenu = pauseMenu;
            return;
        }

        if (inMainMenu == false && nextMenu == pauseMenu)
        {
            DeactivateAllMenus();
            pauseMenu.SetActive(true);
            inPauseMenu = true;
            MiddleText.text = "Game Is Paused";
            nextMenu = null;
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
