using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Damage_Stacking")]
public class Note_E_Damage_Stacking : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        int stackedDamage = combatHandler.StackDamage(note, GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
        combatHandler.DealDamage(note, stackedDamage);
    }

    public override int GetTotalStat(int ownedCount)
    {
        return damage + upgradeDamage * Mathf.Max(0, ownedCount - 1);
    }
}
