using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 1.0f;
        [SerializeField] private float lifeSpan = 6.0f;


        private Vector3 fixedForward = new Vector3(0, 90, 0);

        private Rigidbody rb = null;
        private TrailRenderer trail = null;

        private float lifeTime = 0.0f;

        public static int BulletCount { private set; get; } = 0;
        private void Awake()
        {
            BulletCount++;
            rb = GetComponent<Rigidbody>();
            trail = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            lifeTime += Time.deltaTime;
            if(lifeTime>lifeSpan)
            {
                DestroyBullet();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bomb")
            {
                collision.collider.GetComponent<Bomb>().GetDestroyed();
                //Tirar particulas
                DestroyBullet();
            }
            if (collision.collider.tag == "Limit")
            {
                //Tirar Particulas
                DestroyBullet();
            }
        }
        public void ResetBullet(Transform spawnPoint)
        {
            gameObject.SetActive(true);
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            rb.AddForce(transform.right * bulletSpeed, ForceMode.Impulse);
            lifeTime = 0.0f;
        }

        private void DestroyBullet()
        {
            gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
            trail.Clear();
        }
    }
}