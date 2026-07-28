using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Damage_Stacking")]
public class Note_E_Damage_Stacking : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note)
    {
        int stackedDamage = noteEffectHandler.StackDamage(GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)), note);
        noteEffectHandler.DealDamage(note, stackedDamage);
    }

    public override int GetTotalStat(int ownedCount)
    {
        return damage + upgradeDamage * (ownedCount - 1);
    }
}
