using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFollower : IBombBehavior
{
    private Rigidbody rb;
    private Transform playerTransform;
    private float chaseSpeed = 10f;

    public BombFollower(GameObject bomb, Transform player)
    {
        rb = bomb.GetComponent<Rigidbody>();
        playerTransform = player;
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
