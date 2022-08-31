using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameStartAnimation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Image blackPanel;
    public GameObject allGameLogic;

    float yMin;
    float padding = 1f;
    float playerPosY;
    void Awake()
    {
        Time.timeScale = 1;
        DOTween.Init();
        //blackPanel.color = Color.black;
        //blackPanel.gameObject.SetActive(true);
        playerPosY = player.position.y;
        MoveBorders();
        player.position = new Vector2(player.position.x, yMin);
        player.DOMoveY(playerPosY, 2f).OnComplete(OnGame);
        DOTween.To(() => blackPanel.color, x => blackPanel.color = x, new Color(0f,0f,0f,0f), 2f).SetDelay(.5f);
    }

    public void MoveBorders()
    {
        Camera gameCamera = Camera.main;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, -1, 0)).y - padding;
    }
    void OnGame()
    {
        allGameLogic.SetActive(true);
        blackPanel.gameObject.SetActive(false);
    }
}
