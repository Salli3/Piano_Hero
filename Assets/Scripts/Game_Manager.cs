using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Combat_Manager combatManager;
    public Stats_Manager statsManager;

    [SerializeField] private int enemyCounter = 0;

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
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Shop")
        {
            SceneManager.LoadScene("Battle");
        }
    }

    public void IncreaseEnemyCounter()
    {
        enemyCounter++;
        if (enemyCounter >= 3)
        {
            EndCombat();
        }
    }

    private void EndCombat()
    {
        enemyCounter = 0;
        combatManager.isCombatActive = false;
        SceneManager.LoadScene("Shop");
    }
}
