using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Multi_Hit")]
public class Note_E_Multi_Hit : Note_SO
{
    [SerializeField] private float damage;
    [SerializeField] private int hitTime;
    [SerializeField] private int upgradeHitTime;

    public override void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note)
    {
        noteEffectHandler.RunMultiHit(note, damage, (int)GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
    }

    public override float GetTotalStat(int ownedCount)
    {
        return hitTime + upgradeHitTime * (ownedCount - 1);
    }
}
