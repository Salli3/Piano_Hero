using System;
using System.Collections;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    [SerializeField] private Camera_Shake cameraShake;
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private Enemy_UI enemyUI;
    [SerializeField] private Enemy_SO currentEnemy;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;
    [SerializeField] private int moneyReward;

    #region Event subscribers
    private void OnEnable()
    {
        Combat_Manager.DamageEnemy += OnDamageEnemy;
        Enemy_Manager.SpawnNewEnemy += SetEnemy;
    }
    private void OnDisable()
    {
        Combat_Manager.DamageEnemy -= OnDamageEnemy;
        Enemy_Manager.SpawnNewEnemy -= SetEnemy;
    }
    #endregion

    private void OnDamageEnemy(int amount, bool _)
    {
        ChangeHP(amount);
    }

    private void SetEnemy(Enemy_SO newEnemy)
    {
        currentEnemy = newEnemy;
        maxHP = Mathf.CeilToInt(currentEnemy.enemyHP * Game_Manager.instance.EnemyHpMultiplier);
        currentHP = maxHP;
        Game_Manager.instance.currentEnemyHP = currentHP;
        uiHP.UpdateHP(currentHP, maxHP, 0);
        StartCoroutine(enemyUI.EnemyAppear());
    }

    private void ChangeHP(int amount)
    {
        if (currentEnemy == null) return;

        currentHP += amount; 
        if (currentHP >= maxHP) currentHP = maxHP;

        Game_Manager.instance.currentEnemyHP = currentHP; // set reference for other scripts
        if (amount < 0) cameraShake.Shake(); // camera hit shake
        uiHP.UpdateHP(currentHP, maxHP, amount); // update UI

        if (currentHP <= 0 && Game_Manager.instance.isCombatActive == true)
        {
            Game_Manager.instance.isCombatActive = false;
            Game_Manager.instance.statsManager.ModifyStat(Stat_Type.Money, currentEnemy.enemyMoneyReward);
            StartCoroutine(enemyUI.EnemyDefeat(currentEnemy));
        }
    }
}
