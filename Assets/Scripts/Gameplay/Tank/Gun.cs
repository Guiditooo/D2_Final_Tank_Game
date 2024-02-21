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
        [SerializeField] private int bulletAmmount = 5;

        private AudioSource audioSource = null;
        private Animator animator = null;

        private List<GameObject> bulletList = new List<GameObject>();

        private bool hasShoot = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            Aimer.OnAim += Shoot;
        }

        private void Start()
        {
            for (int i = 0; i < bulletAmmount; i++)
            {
                GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletFolder);
                go.SetActive(false);
                bulletList.Add(go);
            }
        }
        private void Update()
        {
            if(hasShoot)
            {
                animator.ResetTrigger("OnShoot");
                hasShoot = false;
            }
        }

        private void OnDestroy()
        {
            Aimer.OnAim -= Shoot;
        }

        private void Shoot(Quaternion rotation)
        {
            foreach (GameObject bullet in bulletList)
            {
                if (bullet.activeSelf) continue;
                else
                {
                    audioSource.Play();
                    MakeShootAnimation();
                    bullet.GetComponent<Bullet>().ResetBullet(bulletSpawnPoint);
                    return;
                }
            }
        }

        private void MakeShootAnimation()
        {
            animator.SetTrigger("OnShoot");
            hasShoot = true;
        }

    }
}