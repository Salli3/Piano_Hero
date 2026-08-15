using UnityEngine;

public abstract class Note_SO : ScriptableObject, IBuy
{
    [SerializeField] private string noteName;
    [SerializeField, TextArea] protected string noteDescription;
    [SerializeField, TextArea] protected string noteUpgradeDescription;
    public float noteSpeed = 10;
    public bool isHostile;
    [SerializeField] protected Color noteColor;

    public string Name => noteName;
    public string Description => GetDescription();
    public int Level => Game_Manager.instance != null ? Game_Manager.instance.statsManager.noteLevelTracker.GetNoteLevel(this) : 0;
    public Color Color => noteColor;

    public void BuyItem()
    {
        Game_Manager.instance.statsManager.noteLevelTracker.PurchaseNote(this);
    }

    public abstract void Apply(Combat_Manager combatManager);
    public abstract int GetTotalStat(int level);
    public virtual string GetDescription()
    {
        if (Level <= 0) return noteDescription;
        return $"{noteUpgradeDescription} (total: {GetTotalStat(Level + 1)})";
    }
}


