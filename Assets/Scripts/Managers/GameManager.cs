using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Time Options")]
    [SerializeField] private float timerMultiplier = 1.0f;
    [SerializeField] private bool bypassSavedTime = false;
    /// <summary>
    /// Bypasses the saved time setted on main menu and uses the fixed inspector time.
    /// </summary>
    [SerializeField] private int fixedInitialTime = 10;

    [Header("Score Options")]
    [SerializeField] private int bombScoreMultiplier = 66660;
    [SerializeField] private int secondScoreMultiplier = 1859;
    [SerializeField] private int minSecondCount = 10;
    [SerializeField] private int minSecondCoef = 10;
    [SerializeField] private int maxSecondCount = 60;
    [SerializeField] private int maxSecondCoef = 1;

    [Header("Bomb Spawner Options")]
    [SerializeField] private BombSpawner bombSpawner = null;
    [SerializeField] private bool bypassBombCount = false;
    /// <summary>
    /// Bypasses the saved bomb count setted on main menu and uses the fixed inspector bomb count.
    /// </summary>
    [SerializeField] private int fixedBombCount = 5;
    [SerializeField] private float distanceFromPlayer = 100.0f;
    [SerializeField] private float bombSpawnDelay = 0.2f;

    [Header("Player Reference")]
    [SerializeField] private Transform playerTransform = null;

    [Header("UI Manager Reference")]
    [SerializeField] private UIController uiController;


    public static System.Action<int> OnTimerChange;
    //public static System.Action<int> OnBombsChange;
    public static System.Action<bool> OnGameOver;
    public static bool GameRunning { get; private set; } = true;

    private float initialTime;
    private float timer;
    private int timerInt;

    private static int bombCount ;
    public static int BombCount { get { return bombCount; } }

    private const string BOMB_KEY = "Ball Count";
    private const string TIME_KEY = "Time";
    public static int TotalScore { get; private set; } = 0;
    public static int BombScore { get; private set; } = 0;
    public static int TimeScore { get; private set; } = 0;
    public static int SecondsCount { get; private set; } = 0;

    private void Awake()
    {
        SetInitialTime();
        SetBombCount();
        timerInt = (int)initialTime;
        timer = 0;
        GameRunning = true;
    }

    private void OnDestroy()
    {
    }

    private void Start()
    {
        bombSpawner?.StartSpawningBombs(bombCount, distanceFromPlayer, playerTransform, bombSpawnDelay);
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
                SecondsCount++;
                if (timerInt < 0)
                {
                    GameOver(false);
                }
                else
                {
                    OnTimerChange?.Invoke(timerInt);
                }
            }
            if (Bomb.BombsDestroyed == BombCount)
            {
                GameOver(true);
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
            initialTime = PlayerPrefs.GetInt(TIME_KEY);
        }
        OnTimerChange?.Invoke((int)initialTime);
    }

    private void SetBombCount()
    {
        if (bypassBombCount)
        {
            bombCount = fixedBombCount;
        }
        else
        {
            bombCount = PlayerPrefs.GetInt(BOMB_KEY);
        }
        uiController.SetBombCounter(bombCount);
    }

    public int GetInitialBombCount() => bombCount;
    private void GameOver(bool hasWon)
    {
        GameRunning = false;
        //Time.timeScale = 0;
        GetTimeMultiplier();
        BombScore = bombScoreMultiplier * Bomb.BombsDestroyed;
        TimeScore = secondScoreMultiplier * (timerInt + 1);
        TotalScore = (BombScore + TimeScore);

        OnGameOver?.Invoke(hasWon);
    }

    private void GetTimeMultiplier()
    {
        secondScoreMultiplier = (int)(minSecondCoef + ((maxSecondCoef - minSecondCoef) / (maxSecondCount - minSecondCount)) * (initialTime - minSecondCount));
    }

    

}