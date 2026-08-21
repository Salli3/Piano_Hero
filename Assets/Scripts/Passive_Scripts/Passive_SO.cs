using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive_SO : ScriptableObject, IBuy
{
    [SerializeField] private string passiveName;
    [SerializeField, TextArea] protected string passiveDescription;
    [SerializeField, TextArea] protected string passiveUpgradeDescription;

    public string Name => passiveName;
    public string Description => GetDescription();
    public int Level => Game_Manager.instance != null ? Game_Manager.instance.statsManager.passiveLevelTracker.GetPassiveLevel(this) : 0;
    public Color Color => Color.white;

    public void BuyItem()
    {
        Game_Manager.instance.statsManager.passiveLevelTracker.PurchasePassive(this);
        Apply();
    }

    public abstract void Apply();
    public abstract int GetTotalStat();
    public virtual string GetDescription()
    {
        if (Level <= 0) return passiveDescription;
        return $"{passiveUpgradeDescription} (total: {GetTotalStat()})";
    }
}