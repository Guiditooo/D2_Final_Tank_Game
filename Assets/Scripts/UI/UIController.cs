using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{

    [SerializeField] private CanvasGroup pausePanel;

    [Header("Menu scene")]
    [SerializeField] private string menuSceneName = "";

    [SerializeField] private TMP_Text timerText = null;

    public static System.Action OnGameOverButtonClick;

    private void Awake()
    {
        PauseSystem.OnPauseStateChange += PausePanelController;
        GameManager.OnTimerChange += UpdateTimerText;
    }
    private void OnDestroy()
    {
        PauseSystem.OnPauseStateChange -= PausePanelController;
        GameManager.OnTimerChange -= UpdateTimerText;
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
    private void ShowPanel(CanvasGroup panel)
    {
        panel.alpha = 1;
        panel.blocksRaycasts = true;
        panel.interactable = true;
    }
    private void HidePanel(CanvasGroup panel)
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

    public void LoadGameOverScene()
    {
        OnGameOverButtonClick();
    }

}

