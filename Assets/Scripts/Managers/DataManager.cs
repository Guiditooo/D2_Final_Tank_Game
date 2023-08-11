using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GameDataConfiguration gameDataConfig = null;//Score and time parameters

        private const int HIGH_SCORE_COUNT = 3;

        private static DataManager instance;
        public static DataManager Instance { get { return instance; } }

        private float sessionTime = 0;

        private PlayerData playerData;//Player related data => name, bomb score & time score
        private GameData gameData;//Game related data => selected time & bombs
        public int GetSessionTime() => (int)sessionTime;

        public float GetHighlightColorDelay() => gameDataConfig.highlightNewScoreColorDelay;

        public int GetInitialTime() => gameData.selectedPlayTime;
        public int GetInitialBombCount() => gameData.selectedBombCount;

        public float GetBombDistanceFromPlayer() => gameDataConfig.distanceFromPlayer;
        public float GetBombSpawnDelay() => gameDataConfig.bombSpawnDelay;

        public int GetBombScore() => playerData.lastBombScore;
        public int GetTimeScore() => playerData.lastTimeScore;
        public int GetTotalScore() => GetBombScore() + GetTimeScore();

        public int GetScorePerBomb() => gameDataConfig.bombScoreMultiplier;
        public int GetScorePerSecond() => gameDataConfig.timeScoreMultiplier;

        public (int, int) GetBombInitialCounts() => (gameDataConfig.minBombCount, gameDataConfig.maxBombCount);
        public (int, int) GetInitialTimeCounts() => (gameDataConfig.minTimeCount, gameDataConfig.maxTimeCount);
        public (int, int) GetBombCoefs() => (gameDataConfig.minBombCoef, gameDataConfig.maxBombCoef);
        public (int, int) GetTimeCoefs() => (gameDataConfig.minTimeCoef, gameDataConfig.maxBombCoef);

        public HighScore[] GetHighScores() => highScores;

        public void SetGameData(int bombQuantity, int timeQuantity)
        {
            gameData.selectedBombCount = bombQuantity;
            gameData.selectedPlayTime = timeQuantity;
        }
        public void SetLastHiScoreUser(string user)
        {
            playerData.lastUser = user;
        }
        public void SavePlayedGameStats(int destroyedBombsCount, int remainingSeconds)
        {
            playerData.destroyedBombCount = destroyedBombsCount;
            playerData.remainingSeconds = remainingSeconds;

            playerData.lastBombScore = GetScorePerBomb() * destroyedBombsCount;
            playerData.lastTimeScore = GetScorePerSecond() * (remainingSeconds + 1);
            playerData.lastTotalScore = playerData.lastBombScore + playerData.lastTimeScore;
        }

        private HighScore[] highScores = new HighScore[HIGH_SCORE_COUNT];


        private void Update()
        {
            sessionTime += Time.deltaTime;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Esto evita que se destruya el objeto cuando cambias de escena
            }
            else
            {
                Destroy(gameObject); // Si ya existe una instancia, destruye este objeto duplicado
            }
        }
    }
}