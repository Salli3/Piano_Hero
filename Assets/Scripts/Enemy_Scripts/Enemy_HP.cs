using System;
using System.Collections;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    [SerializeField] private Enemy_UI enemyUI;
    [SerializeField] private Enemy_SO enemySO;
    [SerializeField] private float currentHP;

    public static event Action OnEnemyDefeated;

    public void SetEnemy(Enemy_SO newEnemy)
    {
        enemySO = newEnemy;
        currentHP = enemySO.enemyHP;
        enemyUI.SetEnemyUI(newEnemy);
    }

    public void ChangeHP(float amount)
    {
        currentHP -= amount;        
        enemyUI.UpdateUI(currentHP, enemySO.enemyHP);

        if (amount > 0)
        {
            enemyUI.Shake();
            
            if (currentHP <= 0 && Game_Manager.instance.combatManager.isCombatActive == true)
            {
                Game_Manager.instance.combatManager.isCombatActive = false;
                StartCoroutine(EnemyDefeat());
            }
        }
    }

    private IEnumerator EnemyDefeat()
    {
        yield return StartCoroutine(enemyUI.EnemyDefeat());
        OnEnemyDefeated?.Invoke();
    }
}
