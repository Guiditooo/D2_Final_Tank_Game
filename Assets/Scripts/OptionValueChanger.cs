using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionValueChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text value = null;
    [SerializeField] private Slider slider = null;
    private void Awake()
    {
        slider.onValueChanged.AddListener(delegate { ChangeValueText();});
        if (PlayerPrefs.HasKey(name))
        { 
            slider.value = PlayerPrefs.GetInt(name);
        }
    }
    public void ChangeValueText()
    {
        value.text = slider.value.ToString();
    }

}
