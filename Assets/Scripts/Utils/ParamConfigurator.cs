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
        public void SaveParameters()
        {
            foreach (OptionValueChanger OVC in GetComponentsInChildren<OptionValueChanger>())
            {
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
            }

            foreach (string key in parameters.Keys)
            {
                PlayerPrefs.SetInt(key, parameters[key]);
                Debug.Log("Saved Followin' Params: " + key + " - " + parameters[key]);
                PlayerPrefs.Save();
            }
        }

    }
}