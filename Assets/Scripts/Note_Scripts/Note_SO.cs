using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note_SO : ScriptableObject
{
    public string noteName;
    public float noteSpeed = 10;
    public bool isHostile;
    public Color noteColor => isHostile ? Color.red : Color.blue;

    public abstract void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note);
}

