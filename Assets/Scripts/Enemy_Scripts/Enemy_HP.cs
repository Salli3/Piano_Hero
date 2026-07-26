using System;
using System.Collections;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    [SerializeField] private Enemy_UI enemyUI;
    [SerializeField] private Enemy_SO enemySO;
    [SerializeField] private float currentHP;
    [SerializeField] private float maxHP;

    public static event Action OnEnemyDefeated;

    public void SetEnemy(Enemy_SO newEnemy)
    {
        enemySO = newEnemy;
        maxHP = enemySO.enemyHP;
        currentHP = maxHP;
        StartCoroutine(EnemyAppear());
    }

    public void ChangeHP(float amount)
    {
        if (enemySO == null) return;

        currentHP -= amount;        
        enemyUI.UpdateUI(currentHP, maxHP);

        if (amount > 0)
        {
            enemyUI.Shake();
            
            if (currentHP <= 0 && Game_Manager.instance.isCombatActive == true)
            {
                Game_Manager.instance.isCombatActive = false;
                StartCoroutine(EnemyDefeat());
            }
        }
    }

    private IEnumerator EnemyAppear()
    {
        enemyUI.SetEnemyUI(enemySO, currentHP, maxHP);
        yield return StartCoroutine(enemyUI.EnemyAppear());
        Game_Manager.instance.isCombatActive = true;
    }

    private IEnumerator EnemyDefeat()
    {
        yield return StartCoroutine(enemyUI.EnemyDefeat());
        Game_Manager.instance.IncreaseEnemyCounter();
        OnEnemyDefeated?.Invoke();
    }
}
