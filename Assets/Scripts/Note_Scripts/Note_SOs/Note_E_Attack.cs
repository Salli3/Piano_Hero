using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack")]
public class Note_E_Attack : Note_SO
{
    [SerializeField] private float damage;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.ApplyAttack(damage + Game_Manager.instance.statsManager.damage);
    }
}
