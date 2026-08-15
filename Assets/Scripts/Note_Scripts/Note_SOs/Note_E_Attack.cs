using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Attack")]
public class Note_E_Attack : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int upgradeDamage;

    private void Reset()
    {
        noteColor = Color.red;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.DealDamage(isHostile, GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return damage + upgradeDamage * Mathf.Max(0, level - 1);
    }
}
