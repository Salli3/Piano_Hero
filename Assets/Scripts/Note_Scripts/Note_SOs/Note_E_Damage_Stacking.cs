using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Damage_Stacking")]
public class Note_E_Damage_Stacking : Note_SO
{
    [SerializeField] private float damage;
    public override void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note)
    {
        float stackedDamage = noteEffectHandler.StackDamage(damage);
        noteEffectHandler.DealDamage(note, stackedDamage);
    }
}
