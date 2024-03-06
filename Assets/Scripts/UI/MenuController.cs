using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GT
{
    public class MenuController : MonoBehaviour
    {
        [Header("Canvas Related")]
        [SerializeField] private CanvasGroup startingPanel;
        [SerializeField] private float fadeSpeed = 1.0f;

        [SerializeField] private TextAsset creditsFile;
        [SerializeField] private TMP_Text creditsText;

        private CanvasGroup actualPanel;

        private void Awake()
        {
            actualPanel = startingPanel;

            foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            actualPanel.alpha = 1;
            actualPanel.blocksRaycasts = true;
            actualPanel.interactable = true;
        }

        private void Start()
        {
            Time.timeScale = 1;
            LoadCredits();
        }

        public void StartPanel(CanvasGroup newPanel)
        {
            StartCoroutine(PanelChange(newPanel));
        }

        IEnumerator MakeItVisible(CanvasGroup panel)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * fadeSpeed;
                panel.alpha = t;
                yield return null;
            }
            panel.blocksRaycasts = true;
            panel.interactable = true;
        }
        IEnumerator MakeItInvisible(CanvasGroup panel)
        {
            panel.blocksRaycasts = false;
            panel.interactable = false;
            float t = 1;
            while (t > 0)
            {
                t -= Time.deltaTime * fadeSpeed;
                panel.alpha = t;
                yield return null;
            }
        }

        IEnumerator PanelChange(CanvasGroup panel)
        {
            yield return StartCoroutine(MakeItInvisible(actualPanel));
            StartCoroutine(MakeItVisible(panel));
            actualPanel = panel;
        }
        public void CloseGame()
        {
            Application.Quit();
        }
        public void LoadGame()
        {
            SceneManager.LoadScene("Gameplay");
        }

        public void LoadHighScore()
        {
            SceneManager.LoadScene("GameOver");
        }

        private void LoadCredits()
        {
            if (creditsText != null && creditsFile != null)
            {
                creditsText.text = creditsFile.text;
            }
        }

    }
}