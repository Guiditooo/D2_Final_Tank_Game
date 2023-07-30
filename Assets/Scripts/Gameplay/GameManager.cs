using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Time Options")]
    [SerializeField] private float timerMultiplier = 1.0f;
    [SerializeField] private bool bypassSavedTime = false;
    [SerializeField] private int fixedInitialTime = 10;
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

    public static int Score { get; private set; }
    public static int SecondsCount { get; private set; } = 0;
    public static int DestroyedBombs { get; private set; } = 0;

    private void Awake()
    {
        Bomb.OnBombDestroy += IncreaseBombCounter;
        SetInitialTime();
        timerInt = (int)initialTime;
        timer = 0;
    }

    private void OnDestroy()
    {
        Bomb.OnBombDestroy -= IncreaseBombCounter;
    }

    private void Update()
    {
        timer += Time.deltaTime * timerMultiplier;
        if (timer > 1)
        {
            timer--;
            timerInt--;
            SecondsCount++;
            if (timerInt < 0)
            {
                GameOver();
            }
            else
            {
                OnTimerChange(timerInt);
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
        OnTimerChange((int)initialTime);
    }

    private void GameOver()
    {
        GameRunning = false;
        Time.timeScale = 0;
        Score = (66660 * DestroyedBombs + 1859 * (60 - SecondsCount));
        OnGameOver();
    }

    private void IncreaseBombCounter()
    {
        DestroyedBombs++;
    }

}