using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagmentService : MonoBehaviour
{
    public Image blackPanel;
    bool animationCompleted = false;
    public float blackInSpeed = 1f;
    public void RestartScene()
    {
        StartCoroutine(RestartSceneAsync());
        blackPanel.gameObject.SetActive(true);
        DOTween.To(() => blackPanel.color, x => blackPanel.color = x, Color.black, blackInSpeed).OnComplete(LoadGame);
        //SceneManager.LoadSceneAsync("SampleScene");
    }
    public void ToMainMenu()
    {
        StartCoroutine(ToMainMenuAsync());
        blackPanel.gameObject.SetActive(true);
        DOTween.To(() => blackPanel.color, x => blackPanel.color = x, Color.black, blackInSpeed).OnComplete(LoadGame);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
    void LoadGame()
    {
        animationCompleted = true;
    }
    IEnumerator RestartSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (animationCompleted)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

    }
    IEnumerator ToMainMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (animationCompleted)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

    }


}
