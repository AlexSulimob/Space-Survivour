using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static GameObject sampleInstance;
        private void Awake()
        {
            if (sampleInstance == null)
            {
                sampleInstance = gameObject;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);

            }
            
        }


    }
}
