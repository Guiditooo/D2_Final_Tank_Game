using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombJumper : IBombBehavior
{
    private Rigidbody rb;
    private float bounceForce = 3f;
    private bool isGrounded = true;

    public BombJumper(GameObject bomb)
    {
        rb = bomb.GetComponent<Rigidbody>();
    }
    public void ExecuteBehavior()
    {
        if (rb != null && isGrounded)
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse); 
            isGrounded = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            isGrounded = true;
        }
    }

}
