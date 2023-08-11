using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GT
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Mixer References")]
        [SerializeField] private AudioMixer audioMixer = null;

        private const string AUDIO_MASTER_KEY = "AMasterK";
        private const string AUDIO_SOUND_KEY = "ASoundK";
        private const string AUDIO_MUSIC_KEY = "AMusicK";

        private const string AM_MASTER_KEY = "Master";
        private const string AM_SOUND_KEY = "Sound";
        private const string AM_MUSIC_KEY = "Music";

        private float minDecibels = -80f;
        private float maxDecibels = 0f;

        private float masterVolume = -30;
        private float soundVolume = -30;
        private float musicVolume = -30;

        private float valueBeforeMutting = 0.0f;

        public static AudioManager instance;

        public bool IsMuted { private set; get; } = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Esto evita que se destruya el objeto cuando cambias de escena
                InitializeAudioSettings();
            }
            else
            {
                Destroy(gameObject); // Si ya existe una instancia, destruye este objeto duplicado
            }
        }

        private void InitializeAudioSettings()
        {
            masterVolume = LookForPlayerPrefs(AUDIO_MASTER_KEY, masterVolume);
            if (masterVolume == minDecibels)
                IsMuted = true;
            soundVolume = LookForPlayerPrefs(AUDIO_SOUND_KEY, soundVolume);
            musicVolume = LookForPlayerPrefs(AUDIO_MUSIC_KEY, musicVolume);
        }

        private void Start()
        {
            audioMixer.SetFloat(AM_MASTER_KEY, masterVolume);
            audioMixer.SetFloat(AM_SOUND_KEY, soundVolume);
            audioMixer.SetFloat(AM_MUSIC_KEY, musicVolume);
        }

        public void SetAudioMasterRef(float master)
        {
            masterVolume = Mathf.Clamp(ConvertSliderValueToDecibel(master), minDecibels, maxDecibels);
            audioMixer.SetFloat(AM_MASTER_KEY, masterVolume);
            IsMuted = masterVolume == minDecibels;
        }
        public void SetAudioSoundRef(float sound)
        {
            soundVolume = Mathf.Clamp(ConvertSliderValueToDecibel(sound), minDecibels, maxDecibels);
            audioMixer.SetFloat(AM_SOUND_KEY, soundVolume);
        }
        public void SetAudioMusicRef(float music)
        {
            musicVolume = Mathf.Clamp(ConvertSliderValueToDecibel(music), minDecibels, maxDecibels);
            audioMixer.SetFloat(AM_MUSIC_KEY, musicVolume);
        }

        public void SetAudioReferences(float master, float sound, float music)
        { //Ahora solo lo hago cuando cierro el panel de opciones
            PlayerPrefs.SetFloat(AUDIO_MASTER_KEY, masterVolume);
            PlayerPrefs.SetFloat(AUDIO_SOUND_KEY, soundVolume);
            PlayerPrefs.SetFloat(AUDIO_SOUND_KEY, musicVolume);
        }

        public void ToggleMute()
        {
            IsMuted = !IsMuted;

            if (IsMuted)
            {
                valueBeforeMutting = masterVolume;
                masterVolume = minDecibels;
            }
            else
            {
                masterVolume = valueBeforeMutting;
                if(masterVolume==minDecibels)
                {
                    masterVolume = maxDecibels;
                }
            }
            audioMixer.SetFloat(AM_MASTER_KEY, masterVolume);
        }

        public float GetMasterVolume() => ConvertDecibelToSliderValue(masterVolume);
        public float GetSoundVolume() => ConvertDecibelToSliderValue(soundVolume);
        public float GetMusicVolume() => ConvertDecibelToSliderValue(musicVolume);

        private float ConvertDecibelToSliderValue(float decibel)
        {
            return Mathf.Pow(10f, (decibel - minDecibels) / (maxDecibels - minDecibels) * Mathf.Log10(101)) - 1;
        }

        private float ConvertSliderValueToDecibel(float sliderValue)
        {
            return minDecibels + (Mathf.Log10(sliderValue) / Mathf.Log10(100)) * (maxDecibels - minDecibels);
        }

        private float LookForPlayerPrefs(string key, float value)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : value;
        }

    }

}