using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bomb : MonoBehaviour
{
    public static System.Action OnBombDestroy;
    protected abstract void Behaviour();
    public static int BombCount { get; private set; } = 0;

    private void Awake()
    {
        BombCount++;
    }
    private void Update()
    {
        Behaviour();
    }
    private void OnDestroy()
    {
        BombCount--;
    }
    public void GetDestroyed()
    {
        Destroy(this.gameObject);
        //Sonido de explosion
        //Particulas de explosion
    }
}
