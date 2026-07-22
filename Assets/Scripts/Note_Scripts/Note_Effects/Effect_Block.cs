using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Effects/Block_Next_Attack")]
public class Effect_Block : Note_Effect
{
    [SerializeField] private int blockTime;
    public override void Apply(Combat_Manager combatManager, Note_SO note)
    {
        combatManager.SetBlockAttack(blockTime);
    }
}
