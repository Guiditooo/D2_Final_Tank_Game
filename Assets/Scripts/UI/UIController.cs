using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace GT
{
    public class UIController : MonoBehaviour
    {
        [Header("Misc")]
        [SerializeField] private float colorMultiplier = 5.0f;

        [Header("Menu scene")]
        [SerializeField] private string menuSceneName = "";
        [SerializeField] private TMP_Text timerText = null;

        [Header("Pause")]
        [SerializeField] private CanvasGroup pausePanel;
        [SerializeField] private Button muteButton = null;
        [SerializeField] private TMP_Text muteButtonText = null;

        [Header("Gameplay")]
        [SerializeField] private CanvasGroup UIPanel = null;
        [SerializeField] private TMP_Text remainingBombs = null;

        [Header("Game Over Section")]
        [SerializeField] private TMP_Text bombsPoints = null;
        [SerializeField] private TMP_Text timerPoints = null;
        [SerializeField] private TMP_Text scorePoints = null;
        [SerializeField] private CanvasGroup gameOverPanel = null;
        [SerializeField] private CanvasGroup summaryPanel = null;
        [SerializeField] private Button summaryNextButton = null;
        [SerializeField] private CanvasGroup nameInputPanel = null;
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button nameInputNextButton = null;
        [SerializeField] private CanvasGroup highScorePanel = null;

        [Header("HighScore Section")]
        [SerializeField] private GT.HighScoreManager highScoreManager = null;

        [SerializeField] private TMP_Text FirstPosScore = null;
        [SerializeField] private TMP_Text SecondPosScore = null;
        [SerializeField] private TMP_Text ThirdPosScore = null;

        [SerializeField] private TMP_Text FirstPosName = null;
        [SerializeField] private TMP_Text SecondPosName = null;
        [SerializeField] private TMP_Text ThirdPosName = null;
        [SerializeField] private Image[] ScoreSlotBG;


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
            summaryNextButton.onClick.RemoveAllListeners();
            nameInputNextButton.onClick.RemoveAllListeners();
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
            StopAllCoroutines();
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

            if (highScoreManager.CompareWithHighScores(GameManager.TotalScore))
            {
                summaryNextButton.onClick.AddListener(LoadInputNamePanel);
            }
            else
            {
                summaryNextButton.onClick.AddListener(LoadHighScores);
            }
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

        private void LoadGameOver(bool hasWon)
        {
            HidePanel(UIPanel);
            ShowPanel(gameOverPanel);
            ShowPanel(summaryPanel);
            CalculateScores();

        }

        private void LoadInputNamePanel()
        {
            HidePanel(summaryPanel);
            ShowPanel(nameInputPanel);
            nameInputNextButton.onClick.AddListener(SaveHighScore);
            nameInputNextButton.onClick.AddListener(LoadHighScores);
        }
        public void LoadHighScores()
        {
            HidePanel(summaryPanel);
            HidePanel(nameInputPanel);

            GT.HighScore[] highScores = highScoreManager.GetHighScores();

            FirstPosName.text = highScores[0].name + " ~";
            SecondPosName.text = highScores[1].name + " ~";
            ThirdPosName.text = highScores[2].name + " ~";

            FirstPosScore.text = highScores[0].score.ToString();
            SecondPosScore.text = highScores[1].score.ToString();
            ThirdPosScore.text = highScores[2].score.ToString();

            ShowPanel(highScorePanel);
        }


        public void SaveHighScore()
        {
            GT.HighScore hs;
            hs.name = nameInputField.text;
            hs.score = GameManager.TotalScore;
            int slotIndex = highScoreManager.InsertInHighScore(hs);
            if (slotIndex >= 3)
            {
                return;
            }
            StartCoroutine(HighLightScoreSlot(slotIndex));
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
            AudioManager.instance.ToggleMute();
            muteButtonText.text = AudioManager.instance.IsMuted ? "Unmute" : "Mute";
        }

        private IEnumerator HighLightScoreSlot(int index)
        {
            Image BG = ScoreSlotBG[index];
            BG.color = Random.ColorHSV(0, 1, 0, 1, 0, 1, 0.20f, 0.35f);
            Color nextColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 0.15f, 0.25f);
            float time = 0;
            while (true)
            {
                time += Time.deltaTime * colorMultiplier;
                Color.Lerp(BG.color, nextColor, time);
                if (time > 1)
                {
                    time--;
                    BG.color = nextColor;
                    nextColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 0.15f, 0.25f);
                }
                yield return new WaitForSeconds(0.65f);
            }
        }

    }

}