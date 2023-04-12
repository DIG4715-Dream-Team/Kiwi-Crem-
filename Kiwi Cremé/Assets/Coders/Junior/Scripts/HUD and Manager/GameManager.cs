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
    public TextMeshProUGUI StatusText { get; private set; }

    private GameObject player;
    private PlayerController Player;

    private GameObject buttonManager;
    private ButtonManager ButtonManager;

    private bool StartTimer = false;

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
        }
        Time.timeScale = 1;
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
        if (ButtonManager.currentScene != "MainMenu")
        {
            Timers();
        }
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