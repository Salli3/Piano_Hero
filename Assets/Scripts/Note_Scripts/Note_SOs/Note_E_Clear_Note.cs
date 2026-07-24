using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Clear_Note")]
public class Note_E_Clear_Note : Note_SO
{
    [SerializeField] private int clearDamage;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.ApplyNoteClear(clearDamage);
    }
}
