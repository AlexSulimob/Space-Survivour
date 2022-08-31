using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Client
{
    public class SetVolume : MonoBehaviour
    {
        public AudioMixer mixer;
        public string exposedParametr;
        public Slider slider;
        
        private void Awake()
        {
            if(!PlayerPrefs.HasKey(exposedParametr))
            {
                PlayerPrefs.SetFloat(exposedParametr, 1);
            }
            mixer.SetFloat(exposedParametr, PlayerPrefs.GetFloat(exposedParametr));
            slider.value = PlayerPrefs.GetFloat(exposedParametr);
        }
        public void SetLevel(float sliderValue)
        {
            mixer.SetFloat(exposedParametr, Mathf.Log10(sliderValue) * 20f);
            PlayerPrefs.SetFloat(exposedParametr, sliderValue);
        }
    }
}
