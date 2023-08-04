using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint = null;
    [SerializeField] private Transform bulletFolder = null;
    [SerializeField] private GameObject bulletPrefab = null;

    private List<Bullet> bulletList = new List<Bullet>();

    private void Awake()
    {
        Aimer.OnAim += Shoot;
    }
    private void OnDestroy()
    {
        Aimer.OnAim -= Shoot;
    }

    private void Shoot(Quaternion rotation)
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletFolder);
    }

}
