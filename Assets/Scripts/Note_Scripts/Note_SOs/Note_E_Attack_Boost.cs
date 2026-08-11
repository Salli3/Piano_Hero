using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_Boost")]
public class Note_E_Attack_Boost : Note_SO
{
    [SerializeField] private int boostTime;
    [SerializeField] private int upgradeBoostTime;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        combatHandler.SetAttackBoost(note, GetTotalStat(Game_Manager.instance.statsManager.noteLevelTracker.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return boostTime + upgradeBoostTime * Mathf.Max(0, ownedCount - 1);
    }
}
