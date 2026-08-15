using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_And_Self_Damage")]
public class Note_E_Attack_Self_Damage : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    [SerializeField] private int selfDamage;
    [SerializeField] private int upgradeSelfDamage;

    private void Reset()
    {
        noteColor = Color.red;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.DealDamage(isHostile, GetTotalDamage(Level));
        combatManager.SelfDamage(isHostile, GetTotalSelfDamage(Level));
    }

    private int GetTotalDamage(int level) => damage + upgradeDamage * Mathf.Max(0, level - 1);
    private int GetTotalSelfDamage(int level) => selfDamage + upgradeSelfDamage * Mathf.Max(0, level - 1);

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
            int totalSelfDamage = GetTotalSelfDamage(Level + 1);
            return $"{noteUpgradeDescription} (damage: {totalDamage}, self damage: {totalSelfDamage})";
        }
    }
}
