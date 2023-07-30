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

    private void Awake()
    {
        SetInitialTime();
        timerInt = (int)initialTime;
        timer = 0;
    }

    private void Update()
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
        OnGameOver();
    }



}