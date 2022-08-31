using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [HideInInspector] public int minimum;
    [HideInInspector] public int maximum;
    [HideInInspector] public int current;
    [SerializeField] Image mask;

    private void Update()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }
}
