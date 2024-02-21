using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GT
{
    public class ParamConfigurator : MonoBehaviour
    {
        [SerializeField] private GameObject configSection = null;
        [SerializeField] private GameObject testingSection = null;
        [SerializeField] private Image tickImage = null;

        private Dictionary<string, int> parameters = new Dictionary<string, int>();

        private DataManager dataManager = null;

        private bool testingMode = false;

        public void SaveParameters()
        {
            PlayerPrefs.SetInt("Testing", testingMode ? 1 : 0);
            if (testingMode)
            {
                dataManager = DataManager.TestingInstance;
            }
            else
            {
                dataManager = DataManager.Instance;


                int bombCount = 0;
                int timeCount = 0;

                foreach (OptionValueChanger OVC in GetComponentsInChildren<OptionValueChanger>())
                { //Esto es hermoso si no tengo que persistir la data con mi DataManager. Cambiar a futuro.
                    int value = int.MaxValue;
                    value = (int)((OVC.GetComponentInChildren<Slider>()).value);
                    if (parameters.ContainsKey(OVC.name))
                    {
                        parameters[OVC.name] = value;
                    }
                    else
                    {
                        parameters.Add(OVC.name, value);
                    }

                    if (OVC.name == "Time") //Esto es horrible :P
                    {
                        timeCount = value;
                    }
                    else
                    {
                        bombCount = value;
                    }
                }

                dataManager.SetGameData(bombCount, timeCount, GameMode.Normal);

                foreach (string key in parameters.Keys)
                {
                    PlayerPrefs.SetInt(key, parameters[key]);
                    PlayerPrefs.Save();
                }
            }
        }

        public void ToggleTestingMode()
        {
            testingMode = !testingMode;
            if (testingMode)
            {
                configSection.SetActive(false);
                testingSection.SetActive(true);
                tickImage.gameObject.SetActive(true);
            }
            else
            {
                configSection.SetActive(true);
                testingSection.SetActive(false);
                tickImage.gameObject.SetActive(false);
            }
        }


    }
}