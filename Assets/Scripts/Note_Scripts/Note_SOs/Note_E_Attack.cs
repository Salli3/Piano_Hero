using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack")]
public class Note_E_Attack : Note_SO
{
    [SerializeField] private int damage;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.ApplyAttack(damage);
    }
}
