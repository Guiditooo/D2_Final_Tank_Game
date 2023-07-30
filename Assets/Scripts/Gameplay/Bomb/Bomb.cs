using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bomb : MonoBehaviour
{
    public static System.Action OnBombDestroy;
    protected abstract void Behaviour();
    private void Update()
    {
        Behaviour();
    }
    private void OnDestroy()
    {
        OnBombDestroy();
    }
    public void GetDestroyed()
    {
        Destroy(this.gameObject);
        //Sonido de explosion
        //Particulas de explosion
    }
}
