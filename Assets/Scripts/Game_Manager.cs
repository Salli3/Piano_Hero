using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Stats_Manager statsManager;

    [Header("Difficulty settings")]
    [SerializeField] private float difficultylevel;
    [SerializeField] private float difficultyMultiplier;

    [Header("Combat info")]
    [SerializeField] private int enemyCounter = 0;
    [SerializeField] private int enemyPerRound = 3;
    [SerializeField] private bool roundEnded;
    public bool isCombatActive;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

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

    public void PickCharacter(Player_SO playerSO)
    {
        statsManager.Initialize(playerSO);
    }

    public void StartCombat()
    {
        SceneManager.LoadScene("Battle");
        roundEnded = false;
    }

    #region Difficulty level setter/getter
    public float GetDifficultyLevel() => difficultylevel;
    private void IncreaseDifficultyLevel() => difficultylevel += difficultyMultiplier;
    #endregion

    #region Combat round management
    public void IncreaseEnemyCounter()
    {
        enemyCounter++;
        if (enemyCounter >= enemyPerRound)
        {
            roundEnded = true;
            IncreaseDifficultyLevel();
            EndCombat();
        }
    }

    public bool IsRoundEnded()
    {
        return roundEnded;
    }

    private void EndCombat()
    {
        enemyCounter = 0;
        isCombatActive = false;
        SceneManager.LoadScene("Shop");
    }
    #endregion
}
