using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Damage_Stacking")]
public class Note_E_Damage_Stacking : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Combat_Manager combatManager, int level)
    {
        int stackedDamage = combatManager.StackDamage(isHostile) * GetTotalStat(level);
        combatManager.DealDamage(isHostile, stackedDamage);
    }

    public override int GetTotalStat(int level)
    {
        return damage + upgradeDamage * Mathf.Max(0, level - 1);
    }
}
