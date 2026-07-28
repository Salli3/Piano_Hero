using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Clear_Note")]
public class Note_E_Clear_Note : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note)
    {
        int cleared = noteEffectHandler.ClearNote();
        noteEffectHandler.DealDamage(note, cleared * GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return damage + upgradeDamage * (ownedCount - 1);
    }
}
