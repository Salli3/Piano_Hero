using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Heal_And_Attack")]
public class Note_E_Heal_And_Attack : Note_SO
{
    [SerializeField] private int healAmount;
    [SerializeField] private int upgradeHeal;
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.Heal(isHostile, GetTotalHeal(Level));
        combatManager.DealDamage(isHostile, GetTotalDamage(Level));
    }

    private int GetTotalHeal(int level) => healAmount + upgradeHeal * Mathf.Max(0, level - 1);
    private int GetTotalDamage(int level) => damage + upgradeDamage * Mathf.Max(0, level - 1);

    public override int GetTotalStat(int level) => GetTotalHeal(level);

    public override string GetDescription()
    {
        if (Level <= 0)
        {
            return noteDescription;
        }
        else
        {
            int totalHeal = GetTotalHeal(Level + 1);
            int totalDamage = GetTotalDamage(Level + 1);
            return $"{noteUpgradeDescription} (heal: {totalHeal}, damage: {totalDamage})";
        }
    }
}