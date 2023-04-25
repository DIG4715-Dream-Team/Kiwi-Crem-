using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool StartTimer = false;

    private GameObject[] Angels;
    private int startingAngels = 0;

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
        Angels = GameObject.FindGameObjectsWithTag("Angel");
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

        if (ButtonManager.currentScene == "Heaven")
        {
            HeavenLogic();
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
        Health.text = $"Current Health:{Player.Health}";
        Timer.text = $"Time Left:{timeLeft.ToString("F1")}";
    }

    private void UpdatePearlAmount()
    {
        EnPearlInfo.text = $"Entry Pearls:{Player.EnPearls}";
        ExPearlInfo.text = $"Exit Pearls:{Player.ExPearls}";
    }

    private void HeavenLogic()
    {
        foreach (GameObject Angel in Angels)
        {
            startingAngels = startingAngels++;
        }

        if (Angels == null)
        {
            Player.UpdatePearl("Heaven");
        }
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
        else if (Player.CompletedObjectives == true)
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
}