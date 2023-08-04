using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    public static AudioManager instance;

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
        soundVolume = LookForPlayerPrefs(AUDIO_SOUND_KEY, soundVolume);
        musicVolume = LookForPlayerPrefs(AUDIO_MUSIC_KEY, musicVolume);

        audioMixer.SetFloat(AM_MASTER_KEY, ConvertSliderValueToDecibel(masterVolume));
        audioMixer.SetFloat(AM_SOUND_KEY, ConvertSliderValueToDecibel(soundVolume));
        audioMixer.SetFloat(AM_MUSIC_KEY, ConvertSliderValueToDecibel(musicVolume));

    }

    public void SetAudioMasterRef(float master)
    {
        masterVolume = Mathf.Clamp(ConvertSliderValueToDecibel(master), minDecibels, maxDecibels);
        PlayerPrefs.SetFloat(AUDIO_MASTER_KEY, masterVolume);
        audioMixer.SetFloat(AM_MASTER_KEY, masterVolume);
    }
    public void SetAudioSoundRef(float sound)
    {
        soundVolume = Mathf.Clamp(ConvertSliderValueToDecibel(sound), minDecibels, maxDecibels);
        PlayerPrefs.SetFloat(AUDIO_SOUND_KEY, soundVolume);
        audioMixer.SetFloat(AM_SOUND_KEY, soundVolume);
    }
    public void SetAudioMusicRef(float music)
    {
        musicVolume = Mathf.Clamp(ConvertSliderValueToDecibel(music), minDecibels, maxDecibels);
        PlayerPrefs.SetFloat(AUDIO_SOUND_KEY, musicVolume);
        audioMixer.SetFloat(AM_MUSIC_KEY, musicVolume);
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
