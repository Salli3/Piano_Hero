using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Multi_Hit")]
public class Note_E_Multi_Hit : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int hitTime;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.ApplyMultiHit(damage, hitTime);
    }
}
