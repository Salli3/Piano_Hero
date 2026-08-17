using System;
using System.Collections;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private Enemy_UI enemyUI;
    [SerializeField] private Enemy_SO currentEnemy;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;
    [SerializeField] private int moneyReward;

    public static event Action<Enemy_SO> OnEnemyDefeated;

    private void OnEnable() => Combat_Manager.DamageEnemy += OnDamageEnemy;
    private void OnDisable() => Combat_Manager.DamageEnemy -= OnDamageEnemy;

    private void OnDamageEnemy(int amount, bool _)
    {
        ChangeHP(amount);
    }

    public void SetEnemy(Enemy_SO newEnemy)
    {
        currentEnemy = newEnemy;
        maxHP = Mathf.RoundToInt(currentEnemy.enemyHP * Game_Manager.instance.EnemyHpMultiplier);
        currentHP = maxHP;
        uiHP.UpdateHP(currentHP, maxHP);
        StartCoroutine(EnemyAppear());
    }

    private void ChangeHP(int amount)
    {
        if (currentEnemy == null) return;

        currentHP += amount;
        if (currentHP >= maxHP) currentHP = maxHP;
        uiHP.UpdateHP(currentHP, maxHP, amount);

        if (currentHP <= 0 && Game_Manager.instance.isCombatActive == true)
        {
            Game_Manager.instance.isCombatActive = false;
            Game_Manager.instance.statsManager.ModifyStat(Stat_Type.Money, currentEnemy.enemyMoneyReward);
            StartCoroutine(EnemyDefeat());
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
        Game_Manager.instance.roundManager.IncreaseEnemyCounter();
        if (Game_Manager.instance.roundManager.IsRoundEnded == false)
        {
            OnEnemyDefeated?.Invoke(currentEnemy);
        }
    }
}
