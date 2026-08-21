using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack_Damage_Stacking")]
public class Note_E_Attack_Damage_Stacking : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;

    private void Reset()
    {
        noteColor = Color.red;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        int stackedDamage = combatManager.StackDamage(isHostile) * GetTotalStat(Level);
        combatManager.DealDamage(isHostile, stackedDamage);
    }

    public override int GetTotalStat(int level)
    {
        return damage + upgradeDamage * Mathf.Max(0, level - 1);
    }
}
