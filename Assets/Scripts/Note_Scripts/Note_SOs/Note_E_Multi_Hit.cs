using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Multi_Hit")]
public class Note_E_Multi_Hit : Note_SO
{
    [SerializeField] private int damage;
    [SerializeField] private int hitTime;
    [SerializeField] private int upgradeHitTime;

    public override void Apply(Combat_Handler combatHandler, int level)
    {
        combatHandler.RunMultiHit(isHostile, damage, GetTotalStat(level));
    }

    public override int GetTotalStat(int level)
    {
        return hitTime + upgradeHitTime * Mathf.Max(0, level - 1);
    }
}
