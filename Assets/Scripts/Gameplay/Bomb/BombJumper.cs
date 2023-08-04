using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombJumper : MonoBehaviour, IBombBehavior
{
    private Rigidbody rb;
    private float bounceForce = 50f;
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
        if (transform.position.y < 7)
        {
            isGrounded = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limit"))
        {
            isGrounded = true;
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
