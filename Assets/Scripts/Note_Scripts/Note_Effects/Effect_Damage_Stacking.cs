using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Effects/Damage_Stacking")]
public class Effect_Damage_Stacking : Note_Effect
{
    [SerializeField] private int stackingDamage;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.SetStackingDamage(stackingDamage);
    }
}
