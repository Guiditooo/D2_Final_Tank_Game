using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class BombFollower : IBombBehavior
    {
        private Rigidbody rb;
        private Transform playerTransform;
        private BombConfiguration config = null;

        public BombFollower(GameObject bomb, Transform player, BombConfiguration bombConfig)
        {
            rb = bomb.GetComponentInChildren<Rigidbody>();
            playerTransform = player;
            config = bombConfig;
        }
        public void ExecuteBehavior()
        {
            if (playerTransform != null)
            {
                Vector3 directionToPlayer = playerTransform.position - rb.position;
                directionToPlayer.y = 0.0f;

                Vector3 moveDirection = directionToPlayer.normalized;
                Vector3 targetPosition = rb.position + moveDirection * config.chaseSpeed * Time.fixedDeltaTime;
                rb.MovePosition(targetPosition);
            }
        }
    }

}