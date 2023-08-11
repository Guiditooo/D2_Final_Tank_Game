using UnityEngine;
using UnityEngine.SceneManagement;

namespace GT
{
    public class GameManager : MonoBehaviour
    {
        [Header("Time Options")]
        [SerializeField] private float timerMultiplier = 1.0f;
        [SerializeField] private bool bypassSavedTime = false;//Bypasses the saved time setted on main menu and uses the fixed inspector time.
        [SerializeField] private int fixedInitialTime = 10;

        [Header("Bomb Spawner Options")]
        [SerializeField] private bool bypassBombCount = false;//Bypasses the saved bomb count setted on main menu and uses the fixed inspector bomb count.
        [SerializeField] private int fixedBombCount = 5;

        [Header("Player Reference")]
        [SerializeField] private Transform playerTransform = null;

        [Header("UI Manager Reference")]
        [SerializeField] private UIController uiController = null;

        [Header("Game Over Scene Reference")]
        [SerializeField] private string gameOverSceneName = "";

        private float initialTime;
        private float timer;
        private int timerInt;
        private int bombCount;
        private bool GameRunning = true;

        private DataManager dataManager = null;
        private BombSpawner bombSpawner = null;

        private void Awake()
        {
            dataManager = DataManager.Instance;
            bombSpawner = BombSpawner.Instance;

            SetInitialTime();
            SetBombCount();
            timerInt = (int)initialTime;
            timer = 0;
            GameRunning = true;
            bombSpawner.OnAllBombsDestroyed += GameOver;
        }
        private void OnDestroy()
        {
            bombSpawner.OnAllBombsDestroyed -= GameOver;
        }

        private void Start()
        {
            bombSpawner?.StartSpawningBombs(bombCount, dataManager.GetBombDistanceFromPlayer(), playerTransform, dataManager.GetBombSpawnDelay());
            uiController.SetBombCounter(bombCount);
        }

        private void Update()
        {
            if (GameRunning)
            {
                timer += Time.deltaTime * timerMultiplier;
                if (timer > 1)
                {
                    timer--;
                    timerInt--;
                    if (timerInt < 0)
                    {
                        GameOver();
                    }
                    else
                    {
                        uiController.UpdateTimerText(timerInt);
                    }
                }
            }
        }

        private void SetInitialTime()
        {
            if (bypassSavedTime)
            {
                initialTime = fixedInitialTime;
            }
            else
            {
                initialTime = dataManager.GetInitialTime();
            }
            uiController.UpdateTimerText(timerInt);
        }

        private void SetBombCount()
        {
            if (bypassBombCount)
            {
                bombCount = fixedBombCount;
            }
            else
            {
                bombCount = dataManager.GetInitialBombCount();
            }
            uiController.SetBombCounter(bombCount);
        }

        private void SaveAllParameters()
        {
            dataManager.SavePlayedGameStats(bombSpawner.DestroyedBombs, timerInt);
        }

        private void GameOver()
        {
            GameRunning = false;
            SaveAllParameters();
            SceneManager.LoadScene(gameOverSceneName);
        }

    }

}