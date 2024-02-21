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

        public System.Action<Bomb> OnGettingDestroyed;

        private void FixedUpdate() //Ambos movimientos los hago con rigidbody
        {
            behavior?.ExecuteBehavior();
        }
        public void GetDestroyed()
        {
            OnGettingDestroyed?.Invoke(this);
            Destroy(this.gameObject);
        }

    }
}