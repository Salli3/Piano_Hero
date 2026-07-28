using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Next_Attack")]
public class Note_E_Block : Note_SO
{
    [SerializeField] private int blockTime;
    [SerializeField] private int upgradeBlockTime;
    public override void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note)
    {
        noteEffectHandler.SetBlock((int)GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
    }

    public override float GetTotalStat(int ownedCount)
    {
        return blockTime + upgradeBlockTime * ownedCount;
    }
}
