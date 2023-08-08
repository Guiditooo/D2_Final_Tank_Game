using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class BombJumper : IBombBehavior
    {
        private Rigidbody rb;
        private BombConfiguration config = null;
        private float bounceForce = 0f;

        public BombJumper(GameObject bomb, BombConfiguration bombConfig)
        {
            rb = bomb.GetComponent<Rigidbody>();
            config = bombConfig;
            bounceForce = config.bounceForce;
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