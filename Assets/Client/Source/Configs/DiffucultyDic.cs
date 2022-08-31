using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffucultyDic : MonoBehaviour
{
    public DiffucultyTime[] diffuculties;
}
[Serializable]
public struct DiffucultyTime
{
    public float timeToNextLevel;
    public DiffucultySettings diffuculty;
}
