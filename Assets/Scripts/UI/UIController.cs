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

        private AudioManager audioManager = null;
        private BombSpawner bombSpawner = null;

        private void Awake()
        {
            foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            PauseSystem.OnPauseStateChange += PausePanelController;
            muteButton.onClick.AddListener(ToggleMute);

            audioManager = AudioManager.instance;
            bombSpawner = BombSpawner.Instance;
            bombSpawner.OnBombDestroy += SetBombCounter;


            muteButtonText.text = audioManager.IsMuted ? "Unmute" : "Mute";



        }
        private void OnDestroy()
        {
            PauseSystem.OnPauseStateChange -= PausePanelController;
            muteButton.onClick.RemoveListener(ToggleMute);
            bombSpawner.OnBombDestroy -= SetBombCounter;
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

        public void UpdateTimerText(int newValue)
        {
            timerText.text = newValue != -1 ? newValue.ToString() : "Unlimited";
        }

        private void UpdateBombCounter()
        {
            remainingBombs.text = bombSpawner.ActualBombs.ToString();
        }

        public void SetBombCounter(int bombCount)
        {
            remainingBombs.text = bombCount != -1 ? bombCount.ToString() : "Testing Mode";
        }

        private void ToggleMute()
        {
            audioManager.ToggleMute();
            muteButtonText.text = audioManager.IsMuted ? "Unmute" : "Mute";
        }

    }

}