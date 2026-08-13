using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Next_Attack")]
public class Note_E_Block : Note_SO
{
    [SerializeField] private int blockTime;
    [SerializeField] private int upgradeBlockTime;
    public override void Apply(Combat_Manager combatManager, int level)
    {
        combatManager.SetBlock(isHostile, GetTotalStat(level));
    }

    public override int GetTotalStat(int level)
    {
        return blockTime + upgradeBlockTime * Mathf.Max(0, level - 1);
    }
}
