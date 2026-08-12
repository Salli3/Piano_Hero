using UnityEngine;

public abstract class Note_SO : ScriptableObject
{
    public string noteName;
    [TextArea] public string noteDescription;
    [TextArea] public string noteUpgradeDescription;
    public float noteSpeed = 10;
    public bool isHostile;
    public Color noteColor => isHostile ? Color.red : Color.blue;

    public AudioClip hitSound;

    public abstract void Apply(Combat_Handler combatHandler, int level);

    public virtual string GetDescription(int level)
    {
        if (level <= 0) return noteDescription;
        return $"{noteUpgradeDescription} (total: {GetTotalStat(level + 1)})";
    }

    public abstract int GetTotalStat(int level);
}

