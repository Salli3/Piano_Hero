using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Stats_Manager statsManager;
    public Round_Manager roundManager;

    public bool isCombatActive;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    #region Data persistent
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
        else
        {
            CleanUpAndDestroy();
            return;
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        Destroy(gameObject);
    }
    #endregion

    public void PickCharacter(Player_SO playerSO)
    {
        statsManager.Initialize(playerSO);
    }

    public void StartCombatScene()
    {
        roundManager.StartNewRound();
        SceneManager.LoadScene("Battle");
    }

    public void StartShopScene()
    {
        isCombatActive = false;
        roundManager.IncreaseDifficulty();
        SceneManager.LoadScene("Shop");
    }
}
