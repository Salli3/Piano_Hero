using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_SO[] enemySOs;

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += PickEnemy;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= PickEnemy;
    }

    private void Start()
    {
        PickEnemy();
    }

    private void PickEnemy(Enemy_SO enemySO = null)
    {
        Enemy_SO[] enemyPool = enemySOs;

        if (enemySO != null)
        {
            enemyPool = enemySOs.Where(e => e != enemySO).ToArray();
        }

        if (enemyPool.Length == 0)
        {
            enemyPool = enemySOs;
        }

        Enemy_SO currentEnemy = enemyPool[Random.Range(0, enemyPool.Length)];

        if (enemyHP != null)
        {
            enemyHP.SetEnemy(currentEnemy);
        }
    }
}
