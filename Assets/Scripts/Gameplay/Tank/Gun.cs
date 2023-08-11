using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform bulletSpawnPoint = null;
        [SerializeField] private Transform bulletFolder = null;
        [SerializeField] private GameObject bulletPrefab = null;

        private AudioSource audioSource = null;

        //private List<Bullet> bulletList = new List<Bullet>();

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            Aimer.OnAim += Shoot;
        }
        private void OnDestroy()
        {
            Aimer.OnAim -= Shoot;
        }

        private void Shoot(Quaternion rotation)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletFolder);
            audioSource.Play();
        }

    }
}