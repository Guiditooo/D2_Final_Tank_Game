using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

namespace GT
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("Misc")]
        [SerializeField] private float colorMultiplier = 5.0f;

        [Header("Menu scene")]
        [SerializeField] private string menuSceneName = "";

        [Header("Summary Section")]
        [SerializeField] private CanvasGroup gameOverPanel = null;
        [SerializeField] private CanvasGroup summaryPanel = null;
        [SerializeField] private TMP_Text bombsPoints = null;
        [SerializeField] private TMP_Text timerPoints = null;
        [SerializeField] private TMP_Text scorePoints = null;
        [SerializeField] private Button summaryNextButton = null;

        [Header("Input Section")]
        [SerializeField] private CanvasGroup nameInputPanel = null;
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button nameInputNextButton = null;

        [Header("HighScore Section")]
        [SerializeField] private CanvasGroup highScorePanel = null;
        [SerializeField] private TMP_Text FirstPosScore = null;
        [SerializeField] private TMP_Text SecondPosScore = null;
        [SerializeField] private TMP_Text ThirdPosScore = null;
        [SerializeField] private TMP_Text FirstPosName = null;
        [SerializeField] private TMP_Text SecondPosName = null;
        [SerializeField] private TMP_Text ThirdPosName = null;
        [SerializeField] private Image[] ScoreSlotBG;

        [SerializeField] private DataManager dataManager = null;
        [SerializeField] private HighScoreManager scoreManager = null;

        private void Awake()
        {
            foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            dataManager = DataManager.Instance;
            scoreManager = new HighScoreManager();
        }
        private void Start()
        {
            ShowPanel(gameOverPanel);
            ShowPanel(summaryPanel);
        }
        private void OnDestroy()
        {
            nameInputNextButton?.onClick.AddListener(SaveHighScore);
            nameInputNextButton?.onClick.AddListener(LoadHighScores);
            summaryNextButton?.onClick.RemoveListener(LoadInputNamePanel);
            summaryNextButton?.onClick.RemoveListener(LoadHighScores);
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
        public void CalculateScores()
        {
            IEnumerator ShowTimeScore, ShowBombScore, ShowTotalScore;
            ShowTimeScore = ShowScore(timerPoints, dataManager.GetTimeScore(), 1.5f);
            ShowBombScore = ShowScore(bombsPoints, dataManager.GetBombScore(), 1.5f);
            ShowTotalScore = ShowScore(scorePoints, dataManager.GetTotalScore(), 1.5f);

            StartCoroutine(ShowTimeScore);
            StartCoroutine(ShowBombScore);
            StartCoroutine(ShowTotalScore);
        }

        public void VerifyNextPanel(bool isHighScore)
        {
            if(isHighScore)
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
            float scorePerFrame = (float)value / timeToShow * Time.deltaTime;
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

            GT.HighScore[] highScores = dataManager.GetHighScores();

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
            hs.score = dataManager.GetTotalScore();
            
            int slotIndex = scoreManager.InsertInHighScore(hs);
            if (slotIndex >= 3)
            {
                return;
            }
            StartCoroutine(HighLightScoreSlot(slotIndex));
        }
        private IEnumerator HighLightScoreSlot(int index)
        {
            Image BG = ScoreSlotBG[index];
            BG.color = Random.ColorHSV(0, 1, 0, 1, 0, 1, 0.20f, 0.25f);
            Color nextColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 0.15f, 0.45f);
            float time = 0;
            while (true)
            {
                time += Time.deltaTime * colorMultiplier;
                Color.Lerp(BG.color, nextColor, time);
                if (time > dataManager.GetBombSpawnDelay())
                {
                    time--;
                    BG.color = nextColor;
                    nextColor = Random.ColorHSV(0.6f, 1, 0.6f, 1, 0.6f, 1, 0.15f, 0.25f);
                }
                yield return new WaitForSeconds(dataManager.GetBombSpawnDelay());
            }
        }

    }



}