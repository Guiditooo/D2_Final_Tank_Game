using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private bool isSelected = false;

        private UnityEngine.UI.Image img = null;
        
        private void Awake()
        {
            img = GetComponent<UnityEngine.UI.Image>();
        }

        private void Update()
        {
            if (isSelected)
            {
                img.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), 0.15f);
            }
        }

    }
}