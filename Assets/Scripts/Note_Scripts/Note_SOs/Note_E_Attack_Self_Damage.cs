using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_And_Self_Damage")]
public class Note_E_Attack_Self_Damage : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    [SerializeField] private int selfDamage;
    [SerializeField] private int upgradeSelfDamage;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        int stacks = Game_Manager.instance.statsManager.noteLevelTracker.GetStackCount(note);
        combatHandler.DealDamage(note, GetTotalDamage(stacks));
        combatHandler.SelfDamage(note, GetTotalSelfDamage(stacks));
    }

    private int GetTotalDamage(int ownedCount) => damage + upgradeDamage * Mathf.Max(0, ownedCount - 1);
    private int GetTotalSelfDamage(int ownedCount) => selfDamage + upgradeSelfDamage * Mathf.Max(0, ownedCount - 1);

    public override int GetTotalStat(int ownedCount) => GetTotalDamage(ownedCount);

    public override string GetDescription(int ownedCount)
    {
        if (ownedCount <= 0)
        {
            return noteDescription;
        }
        else
        {
            int totalDamage = GetTotalDamage(ownedCount + 1);
            int totalSelfDamage = GetTotalSelfDamage(ownedCount + 1);
            return $"{noteUpgradeDescription} (damage: {totalDamage}, self damage: {totalSelfDamage})";
        }
    }
}
