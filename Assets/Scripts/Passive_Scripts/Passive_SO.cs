using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive_SO : ScriptableObject
{
    public string passiveName;
    [TextArea] public string passiveDescription;
    [TextArea] public string passiveUpgradeDescription;

    public abstract void Apply();

    public virtual string GetDescription(int ownedCount)
    {
        if (ownedCount <= 0) return passiveDescription;
        return $"{passiveUpgradeDescription} (total: {GetTotalStat(ownedCount + 1)})";
    }

    public abstract int GetTotalStat(int ownedCount);
}
