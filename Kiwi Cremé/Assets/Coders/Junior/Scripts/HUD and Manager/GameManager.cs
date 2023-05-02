using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI Timer;
    private float timeLeft;
    private float statusTime;
    [SerializeField] private TextMeshProUGUI EnPearlInfo;
    [SerializeField] private TextMeshProUGUI ExPearlInfo;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject healthGoop;
    [SerializeField] private GameObject hudTimer;
    [SerializeField] private GameObject EntryPearl;
    [SerializeField] private GameObject ExitPearl;

    public TextMeshProUGUI StatusText { get; private set; }

    private GameObject player;
    private PlayerController Player;

    private GameObject buttonManager;
    private ButtonManager ButtonManager;

    public bool HellObjective { get; private set; }
    public bool PurgatoryObjective { get; private set; }
    public bool HeavenObjective { get; private set; }
    public bool CompletedObjectives { get; private set; }

    public bool hasHellPearl { get; private set; }
    public bool hasHeavenPearl { get; private set; }
    public bool hasPurgatoryPearl { get; private set; }

    private bool StartTimer = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        timeLeft = 90;
        statusTime = 5;
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager");
        ButtonManager = buttonManager.GetComponent<ButtonManager>();
        StatusText = statusText;
        SceneCheck();
        if (ButtonManager.currentScene != "MainMenu")
        {
            Cursor.lockState = CursorLockMode.Locked;
            player = GameObject.FindGameObjectWithTag("Player");
            Player = player.GetComponent<PlayerController>();
            Transform spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
            Player.transform.position = spawnPoint.position;
            Player.transform.forward = spawnPoint.transform.forward;
        }
        Time.timeScale = 1;
        if (ButtonManager.currentScene == "MainMenu" || ButtonManager.currentScene == "HUB")
        {
            HideHUDObjects();
        }
    }

    void Update()
    {
        if (ButtonManager.currentScene != "MainMenu")
        {
            GameFinishedLogic();
            UpdateHUDElements();
            UpdatePearlAmount();
        }
    }

    private void FixedUpdate()
    {
        if (ButtonManager.currentScene != "MainMenu" && ButtonManager.currentScene != "HUB")
        {
            Timers();
        }
    }

    public void UpdatePearl(string level)
    {
        if (level == "Hell")
        {
            hasHellPearl = true;
        }

        if (level == "Heaven")
        {
            hasHeavenPearl = true;
        }

        if (level == "Purgatory")
        {
            hasPurgatoryPearl = true;
        }
    }

    public void UpdateObjective(string level)
    {
        if (level == "Hell")
        {
            HellObjective = true;
            SceneManager.LoadScene("HUB");
        }

        if (level == "Heaven")
        {
            HeavenObjective = true;
            SceneManager.LoadScene("HUB");
        }

        if (level == "Purgatory")
        {
            PurgatoryObjective = true;
            SceneManager.LoadScene("HUB");
        }
        if (HellObjective == true && HeavenObjective == true && PurgatoryObjective == true)
        {
            CompletedObjectives = true;
        }
    }

    private void HideHUDObjects()
    {
        healthGoop.SetActive(false);
        hudTimer.SetActive(false);
        EntryPearl.SetActive(false);
        ExitPearl.SetActive(false);
    }

    private void SceneCheck()
    {
        if (ButtonManager.currentScene == "MainMenu")
        {
            ButtonManager.inMainMenu = true;
        }
    }

    private void Timers()
    {
        timeLeft = timeLeft -= Time.deltaTime;
        if (StartTimer == true)
        {
            statusTime = statusTime -= Time.deltaTime;
            if (statusTime <= 0.1f)
            {
                StatusText.text = "";
                StartTimer = false;
                statusTime = 5;
            }
        }
    }

    private void UpdateHUDElements()
    {
        Health.text = $"Lives:{Player.Health}";
        Timer.text = $"Time Left:{timeLeft.ToString("F1")}";
    }

    private void UpdatePearlAmount()
    {
        EnPearlInfo.text = $"Entry Pearls:{Player.EnPearls}";
        ExPearlInfo.text = $"Exit Pearls:{Player.ExPearls}";
    }

    private void GameFinishedLogic()
    {
        if (Player.Died == true)
        {
            Time.timeScale = 0;
            ButtonManager.EndMenu.SetActive(true);
            ButtonManager.MiddleText.text = "You have died!";
            Cursor.lockState = CursorLockMode.None;
        }
        else if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().CompletedObjectives == true)
        {
            Time.timeScale = 0;
            ButtonManager.EndMenu.SetActive(true);
            ButtonManager.MiddleText.text = "You completed the level!";
            Cursor.lockState = CursorLockMode.None;
        }
        else if (timeLeft <= 0)
        {
            Time.timeScale = 0;
            ButtonManager.EndMenu.SetActive(true);
            ButtonManager.MiddleText.text = "You failed to complete the objective in time!";
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Status(string condition)
    {
        if (condition == "Entry Portal")
        {
            StatusText.text = "You do not have an entry pearl";
            StartTimer = true;
        }

        if (condition == "Exit Portal")
        {
            StatusText.text = "You do not have an exit pearl";
            StartTimer = true;
        }
    }
    public void ToggleInvincibility()
    {
        Player.isInvincible = !Player.isInvincible;
    }
}