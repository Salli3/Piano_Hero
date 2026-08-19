using UnityEngine;

public class Enemy_MP : MonoBehaviour
{
    [SerializeField] private Combat_Manager combatManager;
    [SerializeField] private UI_MP uiMP;
    [SerializeField] private Enemy_SO currentEnemy;
    [SerializeField] private int currentMP;
    [SerializeField] private int maxMP;

    #region Event subscribers
    private void OnEnable()
    {
        Combat_Manager.EnemyGainMP += ChangeMP;
        Enemy_Manager.SpawnNewEnemy += SetEnemyMP;
    }
    private void OnDisable()
    {
        Combat_Manager.EnemyGainMP -= ChangeMP;
        Enemy_Manager.SpawnNewEnemy -= SetEnemyMP;
    }
    #endregion

    public void SetEnemyMP(Enemy_SO newEnemy)
    {
        currentEnemy = newEnemy;
        maxMP = newEnemy.ultimate.MpCost;
        currentMP = 0;
        ChangeMP(0);
    }

    private void ChangeMP(int amount)
    {
        currentMP += amount;
        uiMP.UpdateMP(currentMP, maxMP, amount);

        if (currentMP >= maxMP)
        {
            currentEnemy.ultimate.Apply(combatManager);
            currentMP -= maxMP;
            uiMP.UpdateMP(currentMP, maxMP, maxMP);
        }
    }
}