using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class Bomb : MonoBehaviour
    {
        private IBombBehavior behavior;
        public void SetBehavior(IBombBehavior newBehavior)
        {
            behavior = newBehavior;
        }
        public static int BombCount { get; private set; } = 0;
        public static int BombsDestroyed { get; private set; } = 0;

        public static System.Action OnGettingDestroyed;

        private void Awake()
        {
            BombCount++;
        }
        private void FixedUpdate() //Ambos movimientos los hago con rigidbody
        {
            behavior?.ExecuteBehavior();
        }
        private void OnDestroy()
        {
            BombCount--;
            BombsDestroyed++;
        }
        public void GetDestroyed()
        {
            //Sonido de explosion
            //Particulas de explosion

            OnGettingDestroyed?.Invoke();
            Destroy(this.gameObject);
        }

    }
}