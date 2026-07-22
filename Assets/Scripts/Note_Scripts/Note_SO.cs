using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Notes/NoteSO")]
public class Note_SO : ScriptableObject
{
    public string noteName;
    public float noteSpeed;
    public float noteDamage;
    public float noteAttackTime;
    public bool isHostile;
    public Color noteColor => isHostile ? Color.red : Color.blue;

    public Note_Effect noteEffect;
}

