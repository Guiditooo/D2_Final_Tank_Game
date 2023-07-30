using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Time Options")]
    [SerializeField] private float timerMultiplier = 1.0f;
    [SerializeField] private bool bypassSavedTime = false;
    [SerializeField] private int fixedInitialTime = 10;

    [Header("Score Options")]
    [SerializeField] private int bombScoreMultiplier = 66660;
    [SerializeField] private int secondScoreMultiplier = 1859;
    [SerializeField] private int minSecondCount = 10;
    [SerializeField] private int minSecondCoef = 10;
    [SerializeField] private int maxSecondCount = 60;
    [SerializeField] private int maxSecondCoef = 1;
    /// <summary>
    /// Bypasses the saved time setted on main menu and uses the fixed inspector time.
    /// </summary>

    public static System.Action<int> OnTimerChange;
    public static System.Action<int> OnBombsChange;
    public static System.Action OnGameOver;
    public static bool GameRunning { get; private set; } = true;

    private float initialTime;
    private float timer;
    private int timerInt;

    public static int TotalScore { get; private set; } = 0;
    public static int BombScore { get; private set; } = 0;
    public static int TimeScore { get; private set; } = 0;
    public static int SecondsCount { get; private set; } = 0;

    private void Awake()
    {
        SetInitialTime();
        timerInt = (int)initialTime;
        timer = 0;
    }

    private void OnDestroy()
    {
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
                if (timerInt <= 0)
                {
                    GameOver();
                }
                else
                {
                    OnTimerChange?.Invoke(timerInt);
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
            initialTime = PlayerPrefs.GetInt("Time");
        }
        OnTimerChange?.Invoke((int)initialTime);
    }

    private void GameOver()
    {
        GameRunning = false;
        //Time.timeScale = 0;
        GetTimeMultiplier();
        BombScore = bombScoreMultiplier * Bomb.BombsDestroyed;
        TimeScore = secondScoreMultiplier * timerInt;
        TotalScore = (BombScore + TimeScore);
        
        OnGameOver?.Invoke();
    }

    private void GetTimeMultiplier()
    {
        secondScoreMultiplier = (int)(minSecondCoef + ((maxSecondCoef - minSecondCoef) / (maxSecondCount - minSecondCount)) * (initialTime - minSecondCount));
    }
}