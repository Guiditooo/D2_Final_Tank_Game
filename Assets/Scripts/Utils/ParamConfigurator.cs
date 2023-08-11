using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GT
{
    public class ParamConfigurator : MonoBehaviour
    {
        private Dictionary<string, int> parameters = new Dictionary<string, int>();

        private DataManager dataManager = null;

        private void Start()
        {
            dataManager = DataManager.Instance;
        }

        public void SaveParameters()
        {
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

            dataManager.SetGameData(bombCount, timeCount);

            foreach (string key in parameters.Keys)
            {
                PlayerPrefs.SetInt(key, parameters[key]);
                Debug.Log("Saved Followin' Params: " + key + " - " + parameters[key]);
                PlayerPrefs.Save();
            }
        }

    }
}