using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Clear_Note")]
public class Note_E_Clear_Note : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Combat_Manager combatManager)
    {
        int cleared = combatManager.ClearNote(isHostile);
        combatManager.DealDamage(isHostile, cleared * GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return damage + upgradeDamage * Mathf.Max(0, level - 1);
    }
}
