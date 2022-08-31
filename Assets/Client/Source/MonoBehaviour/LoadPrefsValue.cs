using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadPrefsValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textAsset;
    [SerializeField] string loadedValue;
    [SerializeField] LoadTypeValue typeValue;
    private void Start()
    {
        if (PlayerPrefs.HasKey(loadedValue))
        {
            switch (typeValue)
            {
                case LoadTypeValue.intT:
                    textAsset.text = PlayerPrefs.GetInt(loadedValue).ToString();
                    break;
                case LoadTypeValue.floatT:
                    textAsset.text = PlayerPrefs.GetFloat(loadedValue).ToString();
                    break;
                case LoadTypeValue.timeT:
                    //textAsset.text = PlayerPrefs.GetFloat(loadedValue).ToString();
                    var ts = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(loadedValue));
                    textAsset.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
                    break;
                default:
                    break;
            }
            

        }
    }
    enum LoadTypeValue
    {
        intT,
        floatT,
        timeT
    }
}
