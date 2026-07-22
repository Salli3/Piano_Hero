using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Effects/Clear_Note")]
public class Effect_Clear_Note : Note_Effect
{
    [SerializeField] private int clearDamage;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.SetNoteClear(clearDamage);
    }
}
