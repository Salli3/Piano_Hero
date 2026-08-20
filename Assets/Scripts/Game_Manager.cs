using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Stats_Manager statsManager;
    public Round_Manager roundManager;
    public UI_Fade fadeUI;

    public bool isCombatActive;

    [SerializeField] private bool isAutoPlay;
    public bool IsAutoPlay => isAutoPlay;

    [Header("Difficulty Settings")]
    [SerializeField] private float difficultyScaler = 1.1f;
    [SerializeField] private int enemyPerRound;
    [SerializeField] private float noteSpeed;
    [SerializeField] private float enemyHpMultiplier;
    [SerializeField] private float enemyDamageMultiplier;
    [SerializeField] private Enemy_SO currentEnemy;
    [SerializeField] private int enemyNoteLevel = 1;
    [SerializeField] private int roundToBoss = 3;
    [SerializeField] private int round = 1;
    public bool IsBossRound => (round % (roundToBoss + 1) == 0);
    public int EnemyPerRound => IsBossRound? 1 : enemyPerRound;
    public float NoteSpeed => noteSpeed * 1.5f;
    public float EnemyHpMultiplier => enemyHpMultiplier;
    public float EnemyDamageMultiplier => enemyDamageMultiplier;
    public Enemy_SO CurrentEnemy => currentEnemy;
    public int currentEnemyHP;

    private bool isNotePaused;
    private float tempNoteSpeed;
    private Coroutine speedUpRoutine;
    [SerializeField] private float returnToNormalSpeedDuration = 1;


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

    public void LoadNextScene()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                StartCombatScene();
                break;
            case 1:
                StartShopScene();
                break;
            case 2:
                StartCombatScene();
                break;
            default:
                StartShopScene();
                break;
        }
    }

    private void StartCombatScene()
    {
        roundManager.StartNewRound();
        SceneManager.LoadScene("Battle");
    }

    private void StartShopScene()
    {
        isCombatActive = false;
        IncreaseDifficulty();
        SceneManager.LoadScene("Shop");
    }

    private void IncreaseDifficulty()
    {
        round++;
        noteSpeed *= difficultyScaler;
        enemyHpMultiplier *= difficultyScaler;
        enemyDamageMultiplier *= difficultyScaler;
        enemyNoteLevel++;
    }

    public void SetEnemy(Enemy_SO enemySO)
    {
        currentEnemy = enemySO;
        statsManager.noteLevelTracker.SetEnemyNote(enemySO, enemyNoteLevel);
    }

    public void PauseNote()
    {
        if (isNotePaused) return;

        if (speedUpRoutine != null)
        {
            StopCoroutine(speedUpRoutine);
            speedUpRoutine = null;
        }

        tempNoteSpeed = noteSpeed;
        noteSpeed = 0;
        isNotePaused = true;
    }

    public void ContinueNote()
    {
        if (!isNotePaused) return;
        isNotePaused = false;

        if (speedUpRoutine != null) StopCoroutine(speedUpRoutine);
        speedUpRoutine = StartCoroutine(GradualSpeedUp());
    }

    private IEnumerator GradualSpeedUp()
    {
        float startSpeed = noteSpeed;
        float targetSpeed = tempNoteSpeed;
        float elapsed = 0f;

        while (elapsed < returnToNormalSpeedDuration)
        {
            float t = Mathf.Clamp01(elapsed / returnToNormalSpeedDuration);
            noteSpeed = Mathf.Lerp(startSpeed, targetSpeed, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        noteSpeed = targetSpeed;
        speedUpRoutine = null;
    }
}
