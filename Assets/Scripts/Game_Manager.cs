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
    public bool isCombatActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Shop")
        {
            SceneManager.LoadScene("Battle");
        }
    }

    #region Difficulty level setter/getter
    public float GetDifficultyLevel() => difficultylevel;
    private void IncreaseDifficultyLevel() => difficultylevel *= difficultyMultiplier;
    #endregion

    #region Combat round management
    public void IncreaseEnemyCounter()
    {
        enemyCounter++;
        IncreaseDifficultyLevel();
        if (enemyCounter >= 3)
        {
            EndCombat();
        }
    }

    public int GetEnemyCounter()
    {
        return enemyCounter;
    }

    private void EndCombat()
    {
        enemyCounter = 0;
        isCombatActive = false;
        SceneManager.LoadScene("Shop");
    }
    #endregion
}
