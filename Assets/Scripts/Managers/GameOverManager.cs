using UnityEngine;

namespace GT
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameOverUI ui = null;
        private DataManager dataManager = null;
        private HighScoreManager scoreManager = null;

        [SerializeField] private AudioSource scoreAudioSource = null;
        [SerializeField] private AudioSource highScoreAudioSource = null;

        private void Awake()
        {
            dataManager = DataManager.Instance;
            scoreManager = new HighScoreManager();
        }

        void Start()
        {
            ui.CalculateScores();
            scoreAudioSource.Play();
            bool isHighScore = scoreManager.CompareWithHighScores(dataManager.GetTotalScore());
            ui.VerifyNextPanel(isHighScore);
            if (isHighScore)
            {
                highScoreAudioSource.Play();
            }
        }
    }
}