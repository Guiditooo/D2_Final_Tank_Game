using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * bulletSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bomb")
        {
            collision.collider.GetComponent<Bomb>().GetDestroyed();
        }
        
    }
}
