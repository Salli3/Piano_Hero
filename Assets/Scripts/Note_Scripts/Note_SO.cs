using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note_SO : ScriptableObject
{
    public string noteName;
    [TextArea] public string noteDescription;
    [TextArea] public string noteUpgradeDescription;
    public float noteSpeed = 10;
    public bool isHostile;
    public Color noteColor => isHostile ? Color.red : Color.blue;

    public abstract void Apply(Note_Effect_Handler noteEffectHandler, Note_SO note);

    public string GetDescription(int ownedCount)
    {
        if (ownedCount <= 0)
        {
            return noteDescription;
        }
        else
        {
            return $"{noteUpgradeDescription} (total: {GetTotalStat(ownedCount + 1)})";
        }
    }

    public abstract int GetTotalStat(int ownedCount);
}

