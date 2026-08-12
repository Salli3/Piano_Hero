using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.alpha = 0;
        gameOverCanvas.interactable = false;
        gameOverCanvas.blocksRaycasts = false;
    }

    public void DisplayGameOverScreen()
    {
        Time.timeScale = 0;
        gameOverCanvas.alpha = 1;
        gameOverCanvas.interactable = true;
        gameOverCanvas.blocksRaycasts = true;
    }

    public void Quit()
    {
        Game_Manager.instance.CleanUpAndDestroy();
        SceneManager.LoadScene("Main_Menu");
    }
}
