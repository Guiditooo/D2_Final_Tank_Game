using UnityEngine;

namespace GT
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BombConfiguration", order = 1)]
    public class BombConfiguration : ScriptableObject
    {
        public int chaseSpeed;
        public int bounceForce;
        public float maxHeight;
        public float minHeight;
    }

}