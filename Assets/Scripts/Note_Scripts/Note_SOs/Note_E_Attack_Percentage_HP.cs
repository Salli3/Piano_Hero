using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_Percentage_HP")]
public class Note_E_Attack_Percent_HP : Note_SO
{
    [SerializeField] private int percentageDamage;
    [SerializeField] private int upgradeDamage;

    private void Reset()
    {
        noteColor = Color.red;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        int targetCurrentHP = combatManager.GetTargetCurrentHP(isHostile);
        combatManager.DealDamage(isHostile, Mathf.CeilToInt(targetCurrentHP * (GetTotalStat(Level) / 100f)));
    }

    public override int GetTotalStat(int level)
    {
        return percentageDamage + upgradeDamage * Mathf.Max(0, level - 1);
    }

    public override string GetDescription()
    {
        if (Level <= 0) return noteDescription;
        return $"{noteUpgradeDescription} (total: {GetTotalStat(Level + 1)}%)";
    }
}
