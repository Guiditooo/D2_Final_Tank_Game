using UnityEngine;

namespace GT
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameOverUI ui = null;
        private DataManager dataManager = null;
        private HighScoreManager scoreManager = null;

        private void Awake()
        {
            dataManager = DataManager.Instance;
            scoreManager = new HighScoreManager();
        }

        void Start()
        {
            ui.CalculateScores();
            ui.VerifyNextPanel(scoreManager.CompareWithHighScores(dataManager.GetTotalScore()));
        }
    }
}