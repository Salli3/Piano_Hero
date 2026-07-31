using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Clear_Note")]
public class Note_E_Clear_Note : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        int cleared = combatHandler.ClearNote();
        combatHandler.DealDamage(note, cleared * GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return damage + upgradeDamage * Mathf.Max(0, ownedCount - 1);
    }
}
