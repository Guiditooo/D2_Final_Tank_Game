using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        masterSlider.value = AudioManager.instance.GetMasterVolume();
        soundSlider.value = AudioManager.instance.GetSoundVolume();
        musicSlider.value = AudioManager.instance.GetMusicVolume();

        masterSlider.onValueChanged.AddListener(SaveMasterSetting);
        soundSlider.onValueChanged.AddListener(SaveSoundSetting);
        musicSlider.onValueChanged.AddListener(SaveMusicSetting);
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveListener(SaveMasterSetting);
        soundSlider.onValueChanged.RemoveListener(SaveSoundSetting);
        musicSlider.onValueChanged.RemoveListener(SaveMusicSetting);
    }

    public void SaveMasterSetting(float sliderValue)
    {
        AudioManager.instance.SetAudioMasterRef(sliderValue);
    }
    public void SaveSoundSetting(float sliderValue)
    {
        AudioManager.instance.SetAudioSoundRef(sliderValue);
    }
    public void SaveMusicSetting(float sliderValue)
    {
        AudioManager.instance.SetAudioMusicRef(sliderValue);
    }
}
