using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class UIController : MonoBehaviour
{

    [SerializeField] private CanvasGroup pausePanel;

    [Header("Menu scene")]
    [SerializeField] private string menuSceneName = "";
    [SerializeField] private TMP_Text timerText = null;
    
    [Header("Gameplay")]
    [SerializeField] private string gameplaySceneName = "";

    [Header("Game Over Section")]
    [SerializeField] private TMP_Text bombsPoints = null;
    [SerializeField] private TMP_Text timerPoints = null;
    [SerializeField] private TMP_Text scorePoints = null;
    [SerializeField] private CanvasGroup summaryPanel = null;
    [SerializeField] private CanvasGroup hiScorePanel = null;

    private void Awake()
    {
        PauseSystem.OnPauseStateChange += PausePanelController;
        GameManager.OnTimerChange += UpdateTimerText;
        GameManager.OnGameOver += LoadGameOver;
    }
    private void OnDestroy()
    {
        PauseSystem.OnPauseStateChange -= PausePanelController;
        GameManager.OnTimerChange -= UpdateTimerText;
        GameManager.OnGameOver -= LoadGameOver;
    }
    private void PausePanelController(PauseStates state)
    {
        if (state == PauseStates.Resumed)
        {
            HidePanel(pausePanel); //if it is resumed, then, i should show the message
        }
        else
        {
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
        while (showingTime<timeToShow)
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
        ShowPanel(summaryPanel);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene();
    }

}

