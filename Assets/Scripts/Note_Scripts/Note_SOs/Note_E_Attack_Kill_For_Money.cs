using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_Kill_For_Money")]
public class Note_E_Attack_Kill_For_Money : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int moneyGain;
    [SerializeField] private int upgradeDamage;
    [SerializeField] private int upgradeMoneyGain;

    private void Reset()
    {
        noteColor = Color.red;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.KillForMoney(isHostile, GetTotalDamage(Level), GetTotalMoneyGain(Level));
    }

    private int GetTotalDamage(int level) => damage + upgradeDamage * Mathf.Max(0, level - 1);
    private int GetTotalMoneyGain(int level) => moneyGain + upgradeMoneyGain * Mathf.Max(0, level - 1);

    public override int GetTotalStat(int level) => GetTotalDamage(level);

    public override string GetDescription()
    {
        if (Level <= 0)
        {
            return noteDescription;
        }
        else
        {
            int totalDamage = GetTotalDamage(Level + 1);
            int totalMoneyGain = GetTotalMoneyGain(Level + 1);
            return $"{noteUpgradeDescription} (damage: {totalDamage}, self damage: {totalMoneyGain})";
        }
    }
}
