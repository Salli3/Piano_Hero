using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Fade : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 0;
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        anim.Play("Fade In");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void EndRound()
    {
        anim.Play("Fade Out");
    }

    public void EndFadeIn()
    {
        Time.timeScale = 1;
        Debug.Log("End fade in");
    }

    public void EndFadeOut()
    {
        Game_Manager.instance.LoadNextScene();
    }
}
