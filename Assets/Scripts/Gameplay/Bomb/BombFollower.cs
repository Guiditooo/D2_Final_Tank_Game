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
        private float chaseSpeed = 0f;

        public BombFollower(GameObject bomb, Transform player, BombConfiguration bombConfig)
        {
            rb = bomb.GetComponent<Rigidbody>();
            playerTransform = player;
            config = bombConfig;
            chaseSpeed = config.chaseSpeed;
        }
        public void ExecuteBehavior()
        {
            if (playerTransform != null)
            {
                Vector3 directionToPlayer = playerTransform.position - rb.position;
                directionToPlayer.y = 0f;

                Vector3 moveDirection = directionToPlayer.normalized;
                Vector3 targetPosition = rb.position + moveDirection * chaseSpeed * Time.deltaTime;
                rb.MovePosition(targetPosition);
            }
        }
    }

}