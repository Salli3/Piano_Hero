using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack")]
public class Note_E_Attack : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        combatHandler.DealDamage(note, GetTotalStat(Game_Manager.instance.statsManager.noteLevelTracker.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return damage + upgradeDamage * Mathf.Max(0, ownedCount - 1);
    }
}
