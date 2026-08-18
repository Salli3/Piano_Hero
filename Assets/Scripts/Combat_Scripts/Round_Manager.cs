using System;
using UnityEngine;

public class Round_Manager : MonoBehaviour
{
    [SerializeField] private bool isRoundEnded;
    [SerializeField] private int enemyCounter = 0;

    private int enemyPerRound => Game_Manager.instance.EnemyPerRound;
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
            Game_Manager.instance.fadeUI.EndRound();
        }
    }
}