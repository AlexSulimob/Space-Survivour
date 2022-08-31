using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public void Release()
    {
        Shared.thunderFactory.ReleaseInstance(gameObject);
    }
}
