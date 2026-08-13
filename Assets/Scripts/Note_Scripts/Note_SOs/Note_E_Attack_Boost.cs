using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_Boost")]
public class Note_E_Attack_Boost : Note_SO
{
    [SerializeField] private int boostTime;
    [SerializeField] private int upgradeBoostTime;
    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.SetAttackBoost(isHostile, GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return boostTime + upgradeBoostTime * Mathf.Max(0, level - 1);
    }
}
