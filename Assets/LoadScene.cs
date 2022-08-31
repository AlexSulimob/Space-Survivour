using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Client
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        public void LoadGame()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
