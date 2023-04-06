using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI Timer;
    private float timeLeft;
    [SerializeField] private TextMeshProUGUI EnPearlInfo;
    [SerializeField] private TextMeshProUGUI ExPearlInfo;
    [SerializeField] private TextMeshProUGUI statusText;
    public TextMeshProUGUI StatusText { get; private set; }

    private GameObject player;
    private PlayerController Player;

    private GameObject buttonManager;
    private ButtonManager ButtonManager;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        timeLeft = 90;
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager");
        ButtonManager = buttonManager.GetComponent<ButtonManager>();
        StatusText = statusText;
        SceneCheck();
        if (ButtonManager.currentScene != "Tiny_Shell_MainMenu")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Player = player.GetComponent<PlayerController>();
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        if (ButtonManager.currentScene != "Tiny_Shell_MainMenu")
        {
            GameFinishedLogic();
            UpdateHUDElements();
            UpdatePearlAmount();
        }
    }

    private void FixedUpdate()
    {
        if (ButtonManager.currentScene != "Tiny_Shell_MainMenu")
        {
            Timers();
        }
    }

    private void SceneCheck()
    {
        if (ButtonManager.currentScene == "Tiny_Shell_MainMenu")
        {
            ButtonManager.inMainMenu = true;
        }
    }

    private void Timers()
    {
        timeLeft = timeLeft -= Time.deltaTime;
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

    public void GameFinishedLogic()
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
        float Timer = 5f;
        Timer -= Time.deltaTime;
        if (Timer <= 0.1f)
        {
            StatusText.text = "";
        }
        if (condition == "Entry Portal")
        {
            StatusText.text = "You do not have an entry pearl";
            float Timer = 5f;
            Timer -= Time.deltaTime;
            if (Timer <= 0.1f)
            {
                StatusText.text = "";
            }
        }

        if (condition == "Exit Portal")
        {
            StatusText.text = "You do not have an exit pearl";
            float Timer = 5f;
            Timer -= Time.deltaTime;
            if (Timer <= 0.1f)
            {
                StatusText.text = "";
            }
        }
    }
}