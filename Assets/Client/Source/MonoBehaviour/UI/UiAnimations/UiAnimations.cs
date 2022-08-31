using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiAnimations : MonoBehaviour
{
    public void ScaleIn()
    {
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutQuart);
    }
    public void ScaleOut()
    {
        transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuart);
    }
}
