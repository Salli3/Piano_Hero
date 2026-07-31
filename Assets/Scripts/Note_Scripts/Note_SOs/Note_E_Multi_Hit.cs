using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Multi_Hit")]
public class Note_E_Multi_Hit : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int hitTime;
    [SerializeField] private int upgradeHitTime;

    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        combatHandler.RunMultiHit(note, damage, GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return hitTime + upgradeHitTime * (ownedCount - 1);
    }
}
