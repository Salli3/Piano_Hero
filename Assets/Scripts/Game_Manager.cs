using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Stats_Manager statsManager;
    public Round_Manager roundManager;

    public bool isCombatActive;

    [Header("Difficulty Settings")]
    [SerializeField] private float difficultyScaler = 1.1f;
    [SerializeField] private int enemyPerRound;
    [SerializeField] private float noteSpeed;  
    [SerializeField] private float enemyHpMultiplier;
    [SerializeField] private float enemyDamageMultiplier;
    public Enemy_SO currentEnemy;

    public int EnemyPerRound => enemyPerRound;
    public float NoteSpeed => noteSpeed;
    public float EnemyHpMultiplier => enemyHpMultiplier;
    public float EnemyDamageMultiplier => enemyDamageMultiplier;

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

    public void CleanUpAndDestroy()
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

    public void SetDifficulty(int enemyPerRound, float noteSpeed, float enemyHpMultiplier, float enemyDamageMultiplier)
    {
        this.enemyPerRound = enemyPerRound;
        this.noteSpeed = noteSpeed;
        this.enemyHpMultiplier = enemyHpMultiplier;
        this.enemyDamageMultiplier = enemyDamageMultiplier;
    }

    public void StartCombatScene()
    {
        roundManager.StartNewRound();
        SceneManager.LoadScene("Battle");
    }

    public void StartShopScene()
    {
        isCombatActive = false;
        IncreaseDifficulty();
        SceneManager.LoadScene("Shop");
    }

    private void IncreaseDifficulty()
    {
        enemyHpMultiplier *= difficultyScaler;
        enemyDamageMultiplier *= difficultyScaler;
    }
}
