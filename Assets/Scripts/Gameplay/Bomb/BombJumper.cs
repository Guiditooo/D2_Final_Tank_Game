using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class BombJumper : IBombBehavior
    {
        private Rigidbody rb;
        private BombConfiguration config = null;
        private bool goingDown = false;

        public BombJumper(GameObject bomb, BombConfiguration bombConfig)
        {
            rb = bomb.GetComponentInChildren<Rigidbody>();
            config = bombConfig;
            goingDown = false;
        }
        public void ExecuteBehavior()
        {
            if (goingDown)
            {
                if (rb.position.y<config.minHeight)
                {
                    goingDown = false;
                }
            }
            else
            {
                if(rb.position.y>config.maxHeight)
                {
                    goingDown = true;
                }
            }

            //int direction = goingDown ? -1 : 1;

            rb.MovePosition(rb.position + (goingDown ? -1 : 1) * config.bounceForce * Vector3.up * Time.fixedDeltaTime);
        }

    }

}