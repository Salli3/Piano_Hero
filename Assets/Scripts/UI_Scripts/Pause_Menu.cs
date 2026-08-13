using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    private bool isPaused = false;

    private void Start()
    {
        pauseCanvasGroup.alpha = 0;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Debug.Log("Escape key pressed!");
                Time.timeScale = 0;
                pauseCanvasGroup.alpha = 1;
                pauseCanvasGroup.interactable = true;
                pauseCanvasGroup.blocksRaycasts = true;
                isPaused = true;
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvasGroup.alpha = 0;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        isPaused = false;
    }

    public void Quit()
    {
        Game_Manager.instance.CleanUpAndDestroy();
        SceneManager.LoadScene("Main_Menu");
    }
}
