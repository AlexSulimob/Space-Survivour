using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuStartGame : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Transform player;
    [SerializeField] Image blackPanel;

    Sequence seq;

    float yMax;
    float padding = 1f;
    bool animationCompleted = false;

    public void StartGame()
    {
        StartCoroutine(LoadYourAsyncScene());
        MoveBorders();
        seq = DOTween.Sequence();
        /*
        seq.Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 1));
        seq.Append(player.DOMoveY(10f, 3f));  
        */

        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 1);
        player.DOMoveY(yMax, 3f);
        DOTween.To(() => blackPanel.color, x => blackPanel.color = x, Color.black, 2f).SetDelay(.5f).OnComplete(LoadGame);



    }
    void LoadGame()
    {
        animationCompleted = true;
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone )
        {
            if(animationCompleted)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    public void MoveBorders()
    {
        Camera gameCamera = Camera.main;

        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + padding;
    }
}
