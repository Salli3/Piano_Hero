using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Heal_And_Attack")]
public class Note_E_Heal_And_Attack : Note_SO
{
    [SerializeField] private int healAmount;
    [SerializeField] private int upgradeHeal;
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;

    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        int stacks = Game_Manager.instance.statsManager.GetStackCount(note);
        combatHandler.Heal(note, GetTotalHeal(stacks));
        combatHandler.DealDamage(note, GetTotalDamage(stacks));
    }

    private int GetTotalHeal(int ownedCount) => healAmount + upgradeHeal * Mathf.Max(0, ownedCount - 1);
    private int GetTotalDamage(int ownedCount) => damage + upgradeDamage * Mathf.Max(0, ownedCount - 1);

    public override int GetTotalStat(int ownedCount) => GetTotalHeal(ownedCount);

    public override string GetDescription(int ownedCount)
    {
        if (ownedCount <= 0)
        {
            return noteDescription;
        }
        else
        {
            int totalHeal = GetTotalHeal(ownedCount + 1);
            int totalDamage = GetTotalDamage(ownedCount + 1);
            return $"{noteUpgradeDescription} (heal: {totalHeal}, damage: {totalDamage})";
        }
    }
}