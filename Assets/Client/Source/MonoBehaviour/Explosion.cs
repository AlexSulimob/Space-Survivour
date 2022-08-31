using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class Explosion : MonoBehaviour
{
    public void Release()
    {
        gameObject.Release();
    }

}
