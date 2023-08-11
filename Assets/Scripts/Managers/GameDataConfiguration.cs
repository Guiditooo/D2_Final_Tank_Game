using UnityEngine;

namespace GT
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameConfiguration", order = 1)]
    public class GameDataConfiguration : ScriptableObject
    {
        [Header("Score Multipliers")]
        public int bombScoreMultiplier;
        public int timeScoreMultiplier;
        [Header("Quantities")]
        public int maxTimeCount;
        public int minTimeCount;
        public int maxBombCount;
        public int minBombCount;
        [Header("Coefficients")]
        public int maxTimeCoef;
        public int minTimeCoef;
        public int maxBombCoef;
        public int minBombCoef;
        [Header("Bomb Configuration")]
        public float distanceFromPlayer; //100.0f
        public float bombSpawnDelay; //0.2f
        [Header("Misc Configuration")]
        public float highlightNewScoreColorDelay;
    }
}