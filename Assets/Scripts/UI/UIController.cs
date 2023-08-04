using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private CanvasGroup pausePanel;

    [Header("Menu scene")]
    [SerializeField] private string menuSceneName = "";
    [SerializeField] private TMP_Text timerText = null;

    [Header("Pause")]
    [SerializeField] private Button muteButton = null;
    [SerializeField] private TMP_Text muteButtonText = null;

    [Header("Gameplay")]
    [SerializeField] private string gameplaySceneName = "";
    [SerializeField] private CanvasGroup UIPanel = null;
    [SerializeField] private TMP_Text remainingBombs = null;

    [Header("Game Over Section")]
    [SerializeField] private TMP_Text bombsPoints = null;
    [SerializeField] private TMP_Text timerPoints = null;
    [SerializeField] private TMP_Text scorePoints = null;
    [SerializeField] private CanvasGroup gameOverPanel = null;
    [SerializeField] private CanvasGroup summaryPanel = null;
    //[SerializeField] private CanvasGroup hiScorePanel = null;

    private void Awake()
    {
        PauseSystem.OnPauseStateChange += PausePanelController;
        GameManager.OnTimerChange += UpdateTimerText;
        GameManager.OnGameOver += LoadGameOver;
        Bomb.OnGettingDestroyed += UpdateBombCounter;
        muteButton.onClick.AddListener(ToggleMute);
    }
    private void OnDestroy()
    {
        PauseSystem.OnPauseStateChange -= PausePanelController;
        GameManager.OnTimerChange -= UpdateTimerText;
        GameManager.OnGameOver -= LoadGameOver;
        Bomb.OnGettingDestroyed -= UpdateBombCounter;
        muteButton.onClick.RemoveListener(ToggleMute);
    }
    private void Start()
    {
        remainingBombs.text = GameManager.BombCount.ToString();
    }
    private void PausePanelController(PauseStates state)
    {
        if (state == PauseStates.Resumed)
        {
            HidePanel(pausePanel); //if it is resumed, then, i should show the message
            ShowPanel(UIPanel);
        }
        else
        {
            HidePanel(UIPanel);
            ShowPanel(pausePanel);
        }
    }
    public void ShowPanel(CanvasGroup panel)
    {
        panel.alpha = 1;
        panel.blocksRaycasts = true;
        panel.interactable = true;
    }
    public void HidePanel(CanvasGroup panel)
    {
        panel.blocksRaycasts = false;
        panel.interactable = false;
        panel.alpha = 0;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    private void UpdateTimerText(int newValue)
    {
        timerText.text = newValue.ToString();
    }

    private void CalculateScores()
    {
        IEnumerator ShowTimeScore, ShowBombScore, ShowTotalScore;
        ShowTimeScore = ShowScore(timerPoints, GameManager.TimeScore, 1.5f);
        ShowBombScore = ShowScore(bombsPoints, GameManager.BombScore, 1.5f);
        ShowTotalScore = ShowScore(scorePoints, GameManager.TotalScore, 1.5f);

        StartCoroutine(ShowTimeScore);
        StartCoroutine(ShowBombScore);
        StartCoroutine(ShowTotalScore);
    }
    IEnumerator ShowScore(TMP_Text text, int value, float timeToShow)
    {
        float scorePerFrame = value / timeToShow;
        float shownValue = 0;
        float showingTime = 0;
        while (showingTime < timeToShow)
        {
            showingTime += Time.deltaTime;
            shownValue += scorePerFrame;
            text.text = shownValue.ToString();
            yield return null;
        }
        text.text = value.ToString();
    }

    private void LoadGameOver()
    {
        HidePanel(UIPanel);
        ShowPanel(gameOverPanel);
        ShowPanel(summaryPanel);
        CalculateScores();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameplaySceneName);
    }

    private void UpdateBombCounter()
    {
        remainingBombs.text = Bomb.BombCount.ToString();
    }

    public void SetBombCounter(int bombCount)
    {
        remainingBombs.text = bombCount.ToString();
    }

    private void ToggleMute()
    {
        muteButtonText.text = AudioManager.instance.IsMuted ? "Unmute" : "Mute";
        AudioManager.instance.ToggleMute();
    }

}

