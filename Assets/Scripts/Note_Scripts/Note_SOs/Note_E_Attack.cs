using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack")]
public class Note_E_Attack : Note_SO
{
    [SerializeField] private float damage;
    public override void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note)
    {
        noteEffectHandler.DealDamage(note, damage);
    }
}
