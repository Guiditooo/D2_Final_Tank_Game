using UnityEngine.UI;
using UnityEngine;

namespace GT
{
    public class ButtonSounder : MonoBehaviour
    {
        private AudioSource buttonSound = null;
        private Button button = null;

        private void Awake()
        {
            button = GetComponent<Button>();
            buttonSound = GetComponent<AudioSource>();
            button.onClick.AddListener(PlaySound);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound()
        {
            buttonSound.Play();
        }

    }

}