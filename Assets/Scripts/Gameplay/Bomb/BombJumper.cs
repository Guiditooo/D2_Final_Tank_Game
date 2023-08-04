using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class BombJumper : MonoBehaviour, IBombBehavior
    {
        private Rigidbody rb;
        private float bounceForce = 15f;

        public BombJumper(GameObject bomb)
        {
            rb = bomb.GetComponent<Rigidbody>();
        }
        public void ExecuteBehavior()
        {
            if (rb != null && rb.velocity.magnitude < 0.05f)
            {
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }

    }

}