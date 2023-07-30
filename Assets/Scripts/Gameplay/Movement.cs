using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float movementSpeed = 1.0f;

    private Rigidbody rb = null;
    private float acceleration = 0;
    private float rotation = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        InputManager.OnMovementPress += SetMovement;
    }
    private void OnDestroy()
    {
        InputManager.OnMovementPress -= SetMovement;
    }

    private void Update()
    {
        if(acceleration!=0)
        {
            rb.AddForce(transform.forward * acceleration * movementSpeed * Time.deltaTime);
        }
        if(rotation !=0)
        {
            rb.AddTorque(Vector3.up * rotation * rotationSpeed * Time.deltaTime);
            //transform.Rotate(thisRot.x, rotationSpeed * Time.deltaTime, thisRot.z);
        }
        acceleration = 0;
        rotation = 0;
    }

    void SetMovement(MovementDirection movementDirection)
    {
        switch (movementDirection)
        {
            case MovementDirection.Backward:
                acceleration = -movementSpeed;
                break;
            case MovementDirection.Forward:
                acceleration = movementSpeed;
                break;
            case MovementDirection.Left:
                rotation = -rotationSpeed;
                break;
            case MovementDirection.Right:
                rotation = rotationSpeed;
                break;
            case MovementDirection.None:
            default:
                acceleration = 0;
                rotation = 0;
                break;
        }
    }


}
