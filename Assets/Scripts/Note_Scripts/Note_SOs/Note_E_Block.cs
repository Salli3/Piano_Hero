using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Next_Attack")]
public class Note_E_Block : Note_SO
{
    [SerializeField] private int blockTime;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.ApplyBlockAttack(blockTime);
    }
}
