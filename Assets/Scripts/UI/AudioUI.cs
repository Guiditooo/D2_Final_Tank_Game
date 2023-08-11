using UnityEngine;
using UnityEngine.UI;

namespace GT
{
    public class AudioUI : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Button exitButton;

        private AudioManager audioManager = null;

        private void Start()
        {
            audioManager = AudioManager.instance;

            masterSlider.value = audioManager.GetMasterVolume();
            soundSlider.value = audioManager.GetSoundVolume();
            musicSlider.value = audioManager.GetMusicVolume();

            masterSlider.onValueChanged.AddListener(SaveMasterSetting);
            soundSlider.onValueChanged.AddListener(SaveSoundSetting);
            musicSlider.onValueChanged.AddListener(SaveMusicSetting);

            exitButton.onClick.AddListener(SaveAudioSettings);
        }

        private void OnDestroy()
        {
            masterSlider.onValueChanged.RemoveListener(SaveMasterSetting);
            soundSlider.onValueChanged.RemoveListener(SaveSoundSetting);
            musicSlider.onValueChanged.RemoveListener(SaveMusicSetting);

            exitButton.onClick.RemoveListener(SaveAudioSettings);
        }

        public void SaveMasterSetting(float sliderValue)
        {
            audioManager.SetAudioMasterRef(sliderValue);
        }
        public void SaveSoundSetting(float sliderValue)
        {
            audioManager.SetAudioSoundRef(sliderValue);
        }
        public void SaveMusicSetting(float sliderValue)
        {
            audioManager.SetAudioMusicRef(sliderValue);
        }
        public void SaveAudioSettings()
        {
            audioManager.SetAudioReferences(masterSlider.value, soundSlider.value, musicSlider.value);
        }
    }
}