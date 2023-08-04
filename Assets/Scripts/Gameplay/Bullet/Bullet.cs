using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.0f;

    private Vector3 fixedForward = new Vector3(0, 90, 0);

    public static int BulletCount { private set; get; } = 0;
    private void Awake()
    {
        BulletCount++;
        transform.forward = transform.forward + fixedForward;
    }

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * bulletSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bomb")
        {
            collision.collider.GetComponent<Bomb>().GetDestroyed();
            //Tirar particulas
            //Hacer sonidito
            Destroy(gameObject);
        }
        if(collision.collider.tag == "Limit")
        {
            //Tirar Particulas
            //Hacer Sonidito
            Destroy(gameObject);
        }
        
    }
}
