using System;
using System.Linq;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] private Enemy_SO[] enemySOs;
    [SerializeField] private Enemy_SO[] bossSOs;

    public static event Action<Enemy_SO> SpawnNewEnemy;

    private void OnEnable()
    {
        Enemy_UI.OnEnemyDefeated += PickEnemy;
    }

    private void OnDisable()
    {
        Enemy_UI.OnEnemyDefeated -= PickEnemy;
    }

    public void PickEnemy(Enemy_SO enemySO = null)
    {
        Enemy_SO[] enemyPool = Game_Manager.instance.IsBossRound ? bossSOs : enemySOs;    

        if (enemySO != null)
        {
            enemyPool = enemySOs.Where(e => e != enemySO).ToArray();
        }

        if (enemyPool.Length == 0)
        {
            enemyPool = enemySOs;
        }

        Enemy_SO currentEnemy = enemyPool[UnityEngine.Random.Range(0, enemyPool.Length)];

        SpawnNewEnemy?.Invoke(currentEnemy);
        Game_Manager.instance.SetEnemy(currentEnemy);
    }
}
