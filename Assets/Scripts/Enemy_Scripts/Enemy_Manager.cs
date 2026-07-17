using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] private Enemy_SO[] enemySOs;
    [SerializeField] private Enemy_HP enemyHP;

    //private void OnEnable()
    //{
    //    Enemy_HP.OnEnemyDefeated += PickEnemy;
    //}

    //private void OnDisable()
    //{
    //    Enemy_HP.OnEnemyDefeated -= PickEnemy;
    //}

    private void Start()
    {
        PickEnemy();
    }

    public void PickEnemy()
    {
        Game_Manager.instance.currentEnemy = enemySOs[Random.Range(0, enemySOs.Length)];
        if (enemyHP != null)
        {
            enemyHP.SetEnemy(Game_Manager.instance.currentEnemy);
        }
        Game_Manager.instance.isCombatActive = true;
    }
}
