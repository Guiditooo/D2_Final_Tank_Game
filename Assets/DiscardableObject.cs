using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class DiscardableObject : MonoBehaviour
    {
        private Bomb bomb = null;
        private AudioSource audioSource = null;

        private bool startedSounding = false;

        private void Awake()
        {
            bomb = GetComponentInChildren<Bomb>();
            audioSource = GetComponent<AudioSource>();
            bomb.OnGettingDestroyed += StartExplotion;
        }

        private void OnDestroy()
        {
            bomb.OnGettingDestroyed -= StartExplotion;
        }

        IEnumerator StartExploding()
        {
            audioSource.Play();
            startedSounding = true;

            while (true)
            {
                if (startedSounding && !audioSource.isPlaying)
                {
                    Destroy(gameObject);
                }
                yield return Time.deltaTime;
            }
        }

        private void StartExplotion(Bomb bomb)
        {
            StartCoroutine(StartExploding());
            //Particulas de explosion
        }

    }

}