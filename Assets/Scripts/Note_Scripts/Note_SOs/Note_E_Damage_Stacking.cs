using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Damage_Stacking")]
public class Note_E_Damage_Stacking : Note_SO
{
    [SerializeField] private float damage;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.ApplyStackingDamage(damage);
    }
}
