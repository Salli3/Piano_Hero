using UnityEngine;

public class Round_Manager : MonoBehaviour
{
    [Header("Difficulty level")]
    [SerializeField] private float difficultyLevel;
    [SerializeField] private float difficultyMultiplier;

    [Header("Combat round")]
    [SerializeField] private int enemyCounter = 0;
    [SerializeField] private int enemyPerRound = 3;
    [SerializeField] private bool isRoundEnded;

    public float DifficultyLevel => difficultyLevel;
    public void IncreaseDifficulty() => difficultyLevel += difficultyMultiplier;

    public bool IsRoundEnded => isRoundEnded;
    public void StartNewRound()
    {
        enemyCounter = 0;
        isRoundEnded = false;
    }
    public void IncreaseEnemyCounter()
    {
        enemyCounter++;
        if (enemyCounter >= enemyPerRound)
        {
            isRoundEnded = true;
            Game_Manager.instance.StartShopScene();
        }
    }
}