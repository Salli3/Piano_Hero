using System.Linq;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_UI enemyUI;
    [SerializeField] private Enemy_SO[] enemySOs;
    [SerializeField] private Enemy_SO[] bossSOs;

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += PickEnemy;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= PickEnemy;
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

        Enemy_SO currentEnemy = enemyPool[Random.Range(0, enemyPool.Length)];

        if (enemyHP != null)
        {
            enemyHP.SetEnemy(currentEnemy);
            enemyUI.SetEnemyUI(currentEnemy);
            Game_Manager.instance.SetEnemy(currentEnemy);
        }
    }
}
