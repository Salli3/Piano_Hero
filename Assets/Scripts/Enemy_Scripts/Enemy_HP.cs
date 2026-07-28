using System;
using System.Collections;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    [SerializeField] private Enemy_UI enemyUI;
    [SerializeField] private Enemy_SO enemySO;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;
    [SerializeField] private int moneyReward;

    public static event Action OnEnemyDefeated;

    public void SetEnemy(Enemy_SO newEnemy)
    {
        enemySO = newEnemy;
        maxHP = enemySO.enemyHP;
        currentHP = maxHP;
        enemyUI.SetEnemyUI(enemySO, currentHP, maxHP);
        StartCoroutine(EnemyAppear());
    }

    public void ChangeHP(int amount)
    {
        if (enemySO == null) return;

        currentHP -= amount;        
        enemyUI.UpdateUI(currentHP, maxHP);

        if (amount > 0)
        {
            enemyUI.HitRespond();
            
            if (currentHP <= 0 && Game_Manager.instance.isCombatActive == true)
            {
                Game_Manager.instance.isCombatActive = false;
                Game_Manager.instance.statsManager.money += moneyReward;
                StartCoroutine(EnemyDefeat());
            }
        }
    }

    private IEnumerator EnemyAppear()
    {
        yield return StartCoroutine(enemyUI.EnemyAppear());
        Game_Manager.instance.isCombatActive = true;
    }

    private IEnumerator EnemyDefeat()
    {
        yield return StartCoroutine(enemyUI.EnemyDefeat());
        Game_Manager.instance.IncreaseEnemyCounter();
        if (Game_Manager.instance.IsRoundEnded() == false)
        {
            OnEnemyDefeated?.Invoke();
        }
    }
}
